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
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Utils.Logger;
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
        protected readonly IClientService Client;
        private readonly INetworkService networkService;
        protected readonly ITrackingService TrackingService;
        protected readonly ICacheService cacheService;
        protected readonly ILoggerService loggerService;
        public static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(60.0);

        protected ServiceBase(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
        {
            Client = clientService;
            this.networkService = networkService;
            TrackingService = trackingService;
            this.cacheService = cacheService;
            this.loggerService = loggerService;
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
            logg.StaticeConsole(LogLevel.INFO, "Response content: {0}");
            var result = JsonConvert.DeserializeObject<T>(stringValue, jsonConverters);
            DefaultLogger.StaticConsole(LogLevel.INFO, "Response content: {0}");
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
        protected bool IsOnline => networkService.IsOnline();

        protected void ClearAllCaches()
        {
            cacheService.OfflineCache.InvalidateAll();
            cacheService.OnlineCache.InvalidateAll();
            cacheService.LocalStorage.InvalidateAll();
        }

        protected async Task<bool> HasCache(string url)
        {
            string key = Client.Host + url + Client.SessionStateKey;
            IEnumerable<string> keys = await cacheService.OnlineCache.GetAllKeys();
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
            string key = Client.Host + url + Client.SessionStateKey;

            var result = await cacheService.OnlineCache.GetOrFetchObject(key, async () =>
            {
                if (IsOnline)
                {
                    HttpResponseMessage httpResponseMessage = await Client.GetAsync(url, timeout, cancellationToken);

                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        var model = DeserializeModel<T>(httpResponseMessage, jsonConverters);
                        loggerService.LogDebug(LogLevel.DEBUG, "Insert Cache key:{0}");
                        await cacheService.OfflineCache.InsertObject(key, model, DateTimeOffset.Now.AddMinutes(CacheService.OfflineCacheMinutes));
                        return model;
                    }
                }
                else
                {
                    return await GetOfflineData<T>(key);
                }

                return null;
            },
            DateTimeOffset.Now.AddMinutes(CacheService.OnlineCacheMinutes));

            if (result == null)
            {
                loggerService.LogConsole(LogLevel.WARN, " {0} response is null");
                await cacheService.OnlineCache.Invalidate(key);
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
            string key = Client.Host + url + Client.SessionStateKey;

            string result = await cacheService.OnlineCache.GetOrFetchObject(key, async () =>
            {
                if (IsOnline)
                {
                    HttpResponseMessage httpResponseMessage = await Client.GetAsync(url, timeout, cancellationToken);

                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        string receivedString = await httpResponseMessage.Content.ReadAsStringAsync();
                        await cacheService.OfflineCache.InsertObject(key, receivedString, DateTimeOffset.Now.AddMinutes(CacheService.OfflineCacheMinutes));
                        loggerService.LogDebug(LogLevel.DEBUG, "Insert Cache key:{0}");
                        return receivedString;
                    }
                }
                else
                {
                    return await GetOfflineData<string>(key);
                }

                return null;
            },
            DateTimeOffset.Now.AddMinutes(CacheService.OnlineCacheMinutes));

            if (result == null)
            {
                loggerService.LogConsole(LogLevel.WARN, " {0} response is null");
                await cacheService.OnlineCache.Invalidate(key);
                return null;
            }

            return result;
        }

        private async Task<T> GetOfflineData<T>(string key) where T : class
        {
            try
            {
                var offlineObject = await cacheService.OfflineCache.GetObject<T>(key);
                loggerService.LogConsole(LogLevel.INFO, "Get Offline cache object for {0} :{1}");
                return offlineObject;
            }
            catch (KeyNotFoundException)
            {
                loggerService.LogConsole(LogLevel.WARN, "Offline cache object for {0} not found");
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
            string key = Client.Host + url + Client.SessionStateKey;

            if (!IsOnline)
            {
                return await GetOfflineData<T>(key);
            }

            HttpResponseMessage httpResponseMessage = await Client.GetAsync(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                    loggerService.LogConsole(LogLevel.INFO, "GetAsync No Cache Response for {0}:{1}");
                    return result;
                }
                catch (Exception exception)
                {
                    TrackingService.TrackException(exception);
                    return null;
                }
            }

            return null;
        }

        protected async Task<string> GetAsyncStringResultNoCache(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage httpResponseMessage = await Client.GetAsync(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                loggerService.LogConsole(LogLevel.INFO, "GetAsync String No Cache response for {0}: {1}");
                return result;
            }
            loggerService.LogConsole(LogLevel.ERROR, "Response for {0} is null");
            return null;
        }

        protected async Task<string> GetAsyncStringResultNoCacheNoHost(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage httpResponseMessage = await Client.GetAsyncNoHost(url, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                loggerService.LogConsole(LogLevel.INFO, "GetAsync String No Cache No Host response for {0}: {1}");
                return result;
            }
            loggerService.LogConsole(LogLevel.ERROR, "Response for {0} is null");
            return null;
        }

        protected async Task<T> PostAsyncNoCache<T>(string url, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null, JsonConverter[] jsonConverters = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await Client.PostAsync(url, content, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                return result;
            }
            loggerService.LogConsole(LogLevel.WARN, "PostAsyncNoCache for {0} is null");
            return null;
        }

        protected async Task<ServiceResponse<T>> PostAsyncNoCacheWithErrorMessage<T>(string url, HttpContent content, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await Client.PostAsync(url, content, timeout);

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
            HttpResponseMessage httpResponseMessage = await Client.PatchAsync(url, content, timeout, cancellationToken);

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
            HttpResponseMessage httpResponseMessage = await Client.PostAsync(url, content, timeout, cancellationToken);

            return httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK;
        }

        protected async Task<T> PatchAsyncNoCache<T>(string url, HttpContent content, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await Client.PatchAsync(url, content, timeout, cancellationToken);

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var result = await Task.Run(() => DeserializeModel<T>(httpResponseMessage, jsonConverters));
                loggerService.LogConsole(LogLevel.INFO, "Patch No cache host response for {0}:{1}");

                return result;
            }

            return null;
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            return await Client.DeleteAsync(url, timeout, cancellationToken);
        }

        protected async Task<ServiceResponse<T>> DeleteAsyncWithErrorMessage<T>(string url, TimeSpan? timeout = null, JsonConverter[] jsonConverters = null, CancellationToken? cancellationToken = null)
            where T : class
        {
            HttpResponseMessage httpResponseMessage = await Client.DeleteAsync(url, timeout, cancellationToken);

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
            loggerService.LogDebug(LogLevel.DEBUG, "Remove online cache for objects from type: {0} with keys starting with: {1}");
            await cacheService.OnlineCache.InvalidateObjectWithKeysStartingWith<T>(urlPrefix);
        }

        protected async Task ClearOnlineCacheForSpecificUrl<T>(string url)
        {
            loggerService.LogDebug(LogLevel.DEBUG, "Remove online cache for object from type: {0} with key:{1}");
            await cacheService.OnlineCache.InvalidateObject<T>(url);
        }

        protected async Task ClearOnlineCacheForObjects<T>()
        {
            loggerService.LogDebug(LogLevel.DEBUG, "Remove online cache for objects from type: {0}");
            await cacheService.OnlineCache.InvalidateAllObjects<T>();
        }
    }
}