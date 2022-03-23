using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
    public class ServiceResponse<T>
        where T : class
    {
        public ErrorResponse Error { get; set; }

        public T Model { get; set; }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }

        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        public static ErrorResponse Empty()
        {
            return new ErrorResponse();
        }

        public string ExtractErrorMessage()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                return Message;
            }

            return ErrorDescription;
        }
    }

    /// <summary>
    /// Base class for all services.
    /// </summary>
    public class ServiceBase
    {
        protected readonly IOptiAPIBaseServiceProvider _optiAPIBaseServiceProvider;

        public static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(60.0);

        protected ServiceBase(IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
        {
            _optiAPIBaseServiceProvider = optiAPIBaseServiceProvider;
        }

        /// <summary>
        /// Deserializes a model into an object.
        /// NOTE: This method is CPU-bound. Consider using it on a different thread.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="httpResponseMessage">The response containing T.</param>
        /// <returns>Populated Object type T</returns>
        public static T DeserializeModel<T>(HttpResponseMessage httpResponseMessage, JsonConverter[] jsonConverters = null)
        {
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(json, jsonConverters);
            return result;
        }

        /// <summary>
        /// Deserializes a model into an object.
        /// NOTE: This method is CPU-bound. Consider using it on a different thread.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="stringValue">String which is representing the object T.</param>
        /// <returns>Populated Object type T</returns>
        protected static T DeserializeModel<T>(string stringValue, JsonConverter[] jsonConverters = null)
        {
            //ToDo replace the DefaultLogger in future commits.
            //DefaultLogger.StaticeConsole(LogLevel.INFO, "Response content: {0}", stringValue);
            var result = JsonConvert.DeserializeObject<T>(stringValue, jsonConverters);
            //DefaultLogger.StaticConsole(LogLevel.INFO, "Response content: {0}", result);
            return result;
        }

        /// <summary>
        /// Serialize a model into an StringContent.
        /// NOTE: This method is CPU-bound. Consider using it on a different thread.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="model">The object that will be serialized into StringContent.</param>
        /// <returns>Populated Object type T</returns>
        public static StringContent SerializeModel(object model)
        {
            string json = JsonConvert.SerializeObject(model);
            StringContent result = new StringContent(json, Encoding.UTF8, "application/json");
            return result;
        }

        /// <summary>
        /// Serialize a model into an StringContent.
        /// NOTE: This method is CPU-bound. Consider using it on a different thread.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="model">The object that will be serialized into StringContent.</param>
        /// <param name="jsonSerializerSettings">Serialization settings that will be apply durring the process.</param>
        /// <returns>Populated Object type T</returns>
        public static StringContent SerializeModel(object model, JsonSerializerSettings jsonSerializerSettings)
        {
            string json = JsonConvert.SerializeObject(model, jsonSerializerSettings);
            StringContent result = new StringContent(json, Encoding.UTF8, "application/json");
            return result;
        }

        /// <summary>
        /// Whether the device has online access
        /// </summary>
        protected bool IsOnline => _optiAPIBaseServiceProvider.GetNetworkService().IsOnline();

        protected void ClearAllCaches()
        {
            _optiAPIBaseServiceProvider.GetCacheService().OfflineCache.InvalidateAll();
            _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.InvalidateAll();
            _optiAPIBaseServiceProvider.GetCacheService().LocalStorage.InvalidateAll();
        }

        protected async Task<bool> HasCache(string url)
        {
            string key = _optiAPIBaseServiceProvider.GetClientService().Host + url + _optiAPIBaseServiceProvider.GetClientService().SessionStateKey;
            IEnumerable<string> keys = await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.GetAllKeys();
            return keys.Contains(key);
        }

        /// <summary>
        /// Fetches a url and caches the deserialized object
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response into</typeparam>
        /// <param name="url">url to GET.</param>
        /// <returns>deserialized object or null</returns>
        /// GetAsyncWithCachedObject
        protected async Task<T> GetAsyncWithCachedResponse<T>(string url, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null) where T : class
        {
            string key = _optiAPIBaseServiceProvider.GetClientService().Host + url + _optiAPIBaseServiceProvider.GetClientService().SessionStateKey;

            var result = await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.GetOrFetchObject(key, async () =>
            {
                if (IsOnline)
                {
                    HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().GetAsync(url, timeout, cancellationToken);

                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        var model = DeserializeModel<T>(httpResponseMessage, jsonConverters);
                        _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Insert Cache key:{0}", key);
                        await _optiAPIBaseServiceProvider.GetCacheService().OfflineCache.InsertObject(key, model, DateTimeOffset.Now.AddMinutes(CacheService.OfflineCacheMinutes));
                        return model;
                    }
                }
                else if(ClientConfig.IsCachingEnabled)
                {
                    return await GetOfflineData<T>(key);
                }

                return null;
            },
            DateTimeOffset.Now.AddMinutes(CacheService.OnlineCacheMinutes));

            if (result == null)
            {
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.WARN, " {0} response is null", null, key);
                await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.Invalidate(key);
                return null;
            }

            return result;
        }

        /// <summary>
        /// Fetches a url and caches the response string
        /// </summary>
        /// <param name="url">url to GET.</param>
        /// <returns>response string object or null</returns>
        /// GetAsyncStringResultWithCachedResponse
        protected async Task<string> GetAsyncStringResultWithCachedResponse(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            string key = _optiAPIBaseServiceProvider.GetClientService().Host + url + _optiAPIBaseServiceProvider.GetClientService().SessionStateKey;

            string result = await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.GetOrFetchObject(key, async () =>
            {
                if (IsOnline)
                {
                    HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().GetAsync(url, timeout, cancellationToken);

                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        string receivedString = await httpResponseMessage.Content.ReadAsStringAsync();
                        await _optiAPIBaseServiceProvider.GetCacheService().OfflineCache.InsertObject(key, receivedString, DateTimeOffset.Now.AddMinutes(CacheService.OfflineCacheMinutes));
                        _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Insert Cache key:{0}", key);
                        return receivedString;
                    }
                }
                else if(ClientConfig.IsCachingEnabled)
                {
                    return await GetOfflineData<string>(key);
                }

                return null;
            },
            DateTimeOffset.Now.AddMinutes(CacheService.OnlineCacheMinutes));

            if (result == null)
            {
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.WARN, " {0} response is null", null, key);
                await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.Invalidate(key);
                return null;
            }

            return result;
        }

        private async Task<T> GetOfflineData<T>(string key) where T : class
        {
            try
            {
                var offlineObject = await _optiAPIBaseServiceProvider.GetCacheService().OfflineCache.GetObject<T>(key);
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "Get Offline cache object for {0} :{1}", null, key, offlineObject);
                return offlineObject;
            }
            catch (KeyNotFoundException)
            {
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.WARN, "Offline cache object for {0} not found", key);
                return null;
            }
        }

        /// <summary>
        /// Fetches a url and caches the string response
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response into</typeparam>
        /// <param name="url">url to GET.</param>
        /// <returns>deserialized object or null</returns>
        protected async Task<T> GetAsyncNoCache<T>(string url, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            string key = _optiAPIBaseServiceProvider.GetClientService().Host + url + _optiAPIBaseServiceProvider.GetClientService().SessionStateKey;

            if (!IsOnline && ClientConfig.IsCachingEnabled)
            {
                return await GetOfflineData<T>(key);
            }

            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().GetAsync(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                    _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "GetAsync No Cache Response for {0}:{1}", url, result);
                    return result;
                }
                catch (Exception exception)
                {
                    _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                    return null;
                }
            }

            return null;
        }

        protected async Task<string> GetAsyncStringResultNoCache(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().GetAsync(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "GetAsync String No Cache response for {0}: {1}", url, result);
                return result;
            }
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.ERROR, "Response for {0} is null", url);
            return null;
        }

        protected async Task<string> GetAsyncStringResultNoCacheNoHost(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().GetAsyncNoHost(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "GetAsync String No Cache No Host response for {0}: {1}", url, result);
                return result;
            }
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.ERROR, "Response for {0} is null", url);
            return null;
        }

        protected async Task<T> PostAsyncNoCache<T>(string url, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null, JsonConverter[] jsonConverters = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().PostAsync(url, content, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                return result;
            }
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.WARN, "PostAsyncNoCache for {0} is null", null, url);
            return null;
        }

        protected async Task<ServiceResponse<T>> PostAsyncNoCacheWithErrorMessage<T>(string url, HttpContent content, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().PostAsync(url, content, timeout);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Model = model };
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(() => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Error = errorResponse };
            }
        }

        protected async Task<ServiceResponse<T>> PatchAsyncNoCacheWithErrorMessage<T>(string url, HttpContent content, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().PatchAsync(url, content, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Model = model };
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(() => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Error = errorResponse };
            }
        }

        protected async Task<bool> PostAsyncNoResult(string url, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().PostAsync(url, content, timeout, cancellationToken);

            return httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK;
        }

        protected async Task<T> PatchAsyncNoCache<T>(string url, HttpContent content, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().PatchAsync(url, content, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "Patch No cache host response for {0}:{1}", null, url, result);

                return result;
            }

            return null;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            return await _optiAPIBaseServiceProvider.GetClientService().DeleteAsync(url, timeout, cancellationToken);
        }

        protected async Task<ServiceResponse<T>> DeleteAsyncWithErrorMessage<T>(string url, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await _optiAPIBaseServiceProvider.GetClientService().DeleteAsync(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Model = model };
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(() => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters));
                return new ServiceResponse<T> { Error = errorResponse };
            }
        }

        protected async Task ClearOnlineCacheForUrlsStartingWith<T>(string urlPrefix)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Remove online cache for objects from type: {0} with keys starting with: {1}", typeof(T).Name, urlPrefix);
            await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.InvalidateObjectWithKeysStartingWith<T>(urlPrefix);
        }

        protected async Task ClearOnlineCacheForSpecificUrl<T>(string url)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Remove online cache for object from type: {0} with key:{1}", typeof(T).Name, url);
            await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.InvalidateObject<T>(url);
        }

        protected async Task ClearOnlineCacheForObjects<T>()
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Remove online cache for objects from type: {0}", typeof(T).Name);
            await _optiAPIBaseServiceProvider.GetCacheService().OnlineCache.InvalidateAllObjects<T>();
        }
    }
}