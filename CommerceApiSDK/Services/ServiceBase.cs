using Akavache;
using CommerceApiSDK.Extensions;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class ServiceResponse<T>
    {
        public ErrorResponse Error { get; set; }

        public T Model { get; set; }

        public Exception Exception { get; set; } = null;

        public HttpStatusCode StatusCode { get; set; }

        public bool IsCached { get; set; } = false;
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
        protected readonly IClientService ClientService;
        protected readonly INetworkService NetworkService;
        protected readonly ITrackingService TrackingService;
        protected readonly ICacheService CacheService;
        protected readonly ILoggerService LoggerService;

        public static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(60.0);

        protected ServiceBase(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService
        )
        {
            this.ClientService = clientService;
            this.NetworkService = networkService;
            this.TrackingService = trackingService;
            this.CacheService = cacheService;
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Deserializes a model into an object.
        /// NOTE: This method is CPU-bound. Consider using it on a different thread.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="httpResponseMessage">The response containing T.</param>
        /// <returns>Populated Object type T</returns>
        public static T DeserializeModel<T>(
            HttpResponseMessage httpResponseMessage,
            JsonConverter[] jsonConverters = null
        )
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
        protected static T DeserializeModel<T>(
            string stringValue,
            JsonConverter[] jsonConverters = null
        )
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
        public static StringContent SerializeModel(
            object model,
            JsonSerializerSettings jsonSerializerSettings
        )
        {
            string json = JsonConvert.SerializeObject(model, jsonSerializerSettings);
            StringContent result = new StringContent(json, Encoding.UTF8, "application/json");
            return result;
        }

        /// <summary>
        /// Whether the device has online access
        /// </summary>
        protected bool IsOnline => NetworkService.IsOnline();

        /// <summary>
        /// Fetches a url and caches the deserialized object
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response into</typeparam>
        /// <param name="url">url to GET.</param>
        /// <returns>deserialized object or null</returns>
        /// GetAsyncWithCachedObject
        protected async Task<ServiceResponse<T>> GetAsyncWithCachedResponse<T>(
            string url,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null,
            CancellationToken? cancellationToken = null
        ) where T : class
        {
            if (!ClientConfig.IsCachingEnabled)
            {
                return await this.GetAsyncNoCache<T>(
                        url,
                        timeout,
                        jsonConverters,
                        cancellationToken
                    );
            }

            string key = this.ClientService.Host + url + this.ClientService.SessionStateKey;

            var result = await this.CacheService.OnlineCache.GetOrFetchObject(
                key,
                async () =>
                {
                    if (IsOnline)
                    {
                        HttpResponseMessage httpResponseMessage = await this.ClientService.GetAsync(
                            url,
                            timeout,
                            cancellationToken
                        );

                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            var model = DeserializeModel<T>(httpResponseMessage, jsonConverters);
                            this.LoggerService.LogDebug(
                                LogLevel.DEBUG,
                                "Insert Cache key:{0}",
                                key
                            );
                            await this.CacheService.OfflineCache.InsertObject(
                                key,
                                model,
                                DateTimeOffset.Now.AddMinutes(
                                    Services.CacheService.OfflineCacheMinutes
                                )
                            );
                            return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
                        }
                        else
                        {
                            var exception = await LogException(httpResponseMessage, url);
                            ErrorResponse errorResponse = await Task.Run(
                                () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                            );
                            return GetServiceResponse<T>(error: errorResponse, exception: exception, statusCode: httpResponseMessage.StatusCode);
                        }
                    }
                    else if (ClientConfig.IsCachingEnabled)
                    {
                        var model = await GetOfflineData<T>(key);
                        return GetServiceResponse<T>(model: model, isCached: true);
                    }
                    return GetServiceResponse<T>();
                },
                DateTimeOffset.Now.AddMinutes(Services.CacheService.OnlineCacheMinutes)
            );

            if (result?.Model == null)
            {
                this.LoggerService.LogConsole(LogLevel.WARN, " {0} response is null", null, key);
                await this.CacheService.OnlineCache.Invalidate(key);
            }

            return result;
        }

        /// <summary>
        /// Fetches a url and caches the response string
        /// </summary>
        /// <param name="url">url to GET.</param>
        /// <returns>response string object or null</returns>
        /// GetAsyncStringResultWithCachedResponse
        protected async Task<ServiceResponse<string>> GetAsyncStringResultWithCachedResponse(
            string url,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            if (!ClientConfig.IsCachingEnabled)
            {
                return await this.GetAsyncStringResultNoCache(url, timeout, cancellationToken);
            }

            string key = this.ClientService.Host + url + this.ClientService.SessionStateKey;

            var result = await this.CacheService.OnlineCache.GetOrFetchObject(
                key,
                async () =>
                {
                    if (IsOnline)
                    {
                        HttpResponseMessage httpResponseMessage = await this.ClientService.GetAsync(
                            url,
                            timeout,
                            cancellationToken
                        );

                        if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            string receivedString =
                                await httpResponseMessage.Content.ReadAsStringAsync();
                            await this.CacheService.OfflineCache.InsertObject(
                                key,
                                receivedString,
                                DateTimeOffset.Now.AddMinutes(
                                    Services.CacheService.OfflineCacheMinutes
                                )
                            );
                            this.LoggerService.LogDebug(
                                LogLevel.DEBUG,
                                "Insert Cache key:{0}",
                                key
                            );
                            return GetServiceResponse<string>(model: receivedString, statusCode: httpResponseMessage.StatusCode);
                        }
                        else
                        {
                           var exp = await LogException(httpResponseMessage, url);
                           return GetServiceResponse<string>(exception: exp, statusCode: httpResponseMessage.StatusCode);
                        }
                    }
                    else if (ClientConfig.IsCachingEnabled)
                    {
                        var model = await GetOfflineData<string>(key);
                        return GetServiceResponse<string>(model: model);
                    }
                    return GetServiceResponse<string>();
                },
                DateTimeOffset.Now.AddMinutes(Services.CacheService.OnlineCacheMinutes)
            );

            if (result?.Model == null)
            {
                this.LoggerService.LogConsole(LogLevel.WARN, " {0} response is null", null, key);
                await this.CacheService.OnlineCache.Invalidate(key);
            }
            return result;
        }

        private async Task<T> GetOfflineData<T>(string key) where T : class
        {
            try
            {
                var offlineObject = await this.CacheService.OfflineCache.GetObject<T>(key);
                this.LoggerService.LogConsole(
                    LogLevel.INFO,
                    "Get Offline cache object for {0} :{1}",
                    null,
                    key,
                    offlineObject
                );
                return offlineObject;
            }
            catch (KeyNotFoundException)
            {
                this.LoggerService.LogConsole(
                    LogLevel.WARN,
                    "Offline cache object for {0} not found",
                    key
                );
                return null;
            }
        }

        /// <summary>
        /// Fetches a url and caches the string response
        /// </summary>
        /// <typeparam name="T">Type to deserialize the response into</typeparam>
        /// <param name="url">url to GET.</param>
        /// <returns>deserialized object or null</returns>
        protected async Task<ServiceResponse<T>> GetAsyncNoCache<T>(
            string url,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null,
            CancellationToken? cancellationToken = null
        ) where T : class
        {
            string key = this.ClientService.Host + url + this.ClientService.SessionStateKey;

            if (!IsOnline && ClientConfig.IsCachingEnabled)
            {
                var model = await GetOfflineData<T>(key);
                return GetServiceResponse<T>(model: model, isCached: true);
            }

            HttpResponseMessage httpResponseMessage = await this.ClientService.GetAsync(
                url,
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var model = await Task.Run(
                        () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                    );
                    this.LoggerService.LogConsole(
                        LogLevel.INFO,
                        "GetAsync No Cache Response for {0}:{1}",
                        url,
                        model
                    );
                    return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
                }
                catch (Exception exception)
                {
                    this.TrackingService.TrackException(exception);
                    return GetServiceResponse<T>(exception: exception, statusCode: httpResponseMessage.StatusCode);
                }
            }
            else
            {
                var exception = await LogException(httpResponseMessage, url);
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error: errorResponse, exception: exception, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<string>> GetAsyncStringResultNoCache(
            string url,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            HttpResponseMessage httpResponseMessage = await this.ClientService.GetAsync(
                url,
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string model = await httpResponseMessage.Content.ReadAsStringAsync();
                this.LoggerService.LogConsole(
                    LogLevel.INFO,
                    "GetAsync String No Cache response for {0}: {1}",
                    url,
                    model
                );
                return GetServiceResponse<string>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                this.LoggerService.LogConsole(LogLevel.ERROR, "Response for {0} is null", url);
                var exception = await LogException(httpResponseMessage, url);
                return GetServiceResponse<string>(exception: exception, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<string>> GetAsyncStringResultNoCacheNoHost(
            string url,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            HttpResponseMessage httpResponseMessage = await this.ClientService.GetAsyncNoHost(
                url,
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string model = await httpResponseMessage.Content.ReadAsStringAsync();
                this.LoggerService.LogConsole(
                    LogLevel.INFO,
                    "GetAsync String No Cache No Host response for {0}: {1}",
                    url,
                    model
                );
                return GetServiceResponse<string>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                this.LoggerService.LogConsole(LogLevel.ERROR, "Response for {0} is null", url);
                var exception = await LogException(httpResponseMessage, url);
                return GetServiceResponse<string>(exception: exception, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<T>> PostAsyncNoCache<T>(
            string url,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null,
            JsonConverter[] jsonConverters = null
        ) where T : class
        {
            var contentForError = string.Empty;
            if (content is StringContent)
            {
                contentForError = await content.ReadAsStringAsync();
            }

            HttpResponseMessage httpResponseMessage = await this.ClientService.PostAsync(
                url,
                string.IsNullOrEmpty(contentForError) ? content : new StringContent(contentForError, Encoding.UTF8, "application/json"),
                timeout,
                cancellationToken
            );

            if (
                httpResponseMessage.StatusCode == HttpStatusCode.Created
                || httpResponseMessage.StatusCode == HttpStatusCode.OK
            )
            {
                var model = await Task.Run(
                    () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                this.LoggerService.LogConsole(
                    LogLevel.WARN,
                    "PostAsyncNoCache for {0} is null",
                    null,
                    url
                );
                var exception = await LogException(httpResponseMessage, url, contentForError);
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error: errorResponse, exception: exception, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<T>> PostAsyncNoCacheWithErrorMessage<T>(
            string url,
            HttpContent content,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null
        ) where T : class
        {
            var contentForError = string.Empty;
            if (content is StringContent)
            {
                contentForError = await content.ReadAsStringAsync();
            }
            HttpResponseMessage httpResponseMessage = await this.ClientService.PostAsync(
                url,
                content,
                timeout
            );

            if (
                httpResponseMessage.StatusCode == HttpStatusCode.Created
                || httpResponseMessage.StatusCode == HttpStatusCode.OK
            )
            {
                var model = await Task.Run(
                    () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error: errorResponse, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<T>> PatchAsyncNoCacheWithErrorMessage<T>(
            string url,
            HttpContent content,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null,
            CancellationToken? cancellationToken = null
        ) where T : class
        {
            HttpResponseMessage httpResponseMessage = await this.ClientService.PatchAsync(
                url,
                content,
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(
                    () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error : errorResponse, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<ServiceResponse<bool>> PostAsyncNoResult(
            string url,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            var contentForError = string.Empty;
            if (content is StringContent)
            {
                contentForError = await content.ReadAsStringAsync();
            }
            HttpResponseMessage httpResponseMessage = await this.ClientService.PostAsync(
                url,
                string.IsNullOrEmpty(contentForError) ? content : new StringContent(contentForError, Encoding.UTF8, "application/json"),
                timeout,
                cancellationToken
            );
            var success = httpResponseMessage.StatusCode == HttpStatusCode.Created
                || httpResponseMessage.StatusCode == HttpStatusCode.OK;

            if (!success)
            {
                var exception = await LogException(httpResponseMessage, url, contentForError);
                return new ServiceResponse<bool>
                {
                    Model = success,
                    Exception = exception,
                    StatusCode = httpResponseMessage.StatusCode
                };
            }

            return new ServiceResponse<bool>
            {
                Model = success,
                StatusCode = httpResponseMessage.StatusCode
            };
        }

        protected async Task<ServiceResponse<T>> PatchAsyncNoCache<T>(
            string url,
            HttpContent content,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null,
            CancellationToken? cancellationToken = null
        ) where T : class
        {
            var contentForError = string.Empty;
            if (content is StringContent)
            {
                contentForError = await content.ReadAsStringAsync();
            }
            HttpResponseMessage httpResponseMessage = await this.ClientService.PatchAsync(
                url,
                string.IsNullOrEmpty(contentForError) ? content : new StringContent(contentForError, Encoding.UTF8, "application/json"),
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(
                    () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                );
                this.LoggerService.LogConsole(
                    LogLevel.INFO,
                    "Patch No cache host response for {0}:{1}",
                    null,
                    url,
                    model
                );

                return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                var exception = await LogException(httpResponseMessage, url, contentForError);
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error: errorResponse, exception: exception, statusCode: httpResponseMessage.StatusCode);
            }
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string url,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            return await this.ClientService.DeleteAsync(url, timeout, cancellationToken);
        }

        protected async Task<ServiceResponse<T>> DeleteAsyncWithErrorMessage<T>(
            string url,
            TimeSpan? timeout = null,
            JsonConverter[] jsonConverters = null,
            CancellationToken? cancellationToken = null
        ) where T : class
        {
            HttpResponseMessage httpResponseMessage = await this.ClientService.DeleteAsync(
                url,
                timeout,
                cancellationToken
            );

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var model = await Task.Run(
                    () => DeserializeModel<T>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(model: model, statusCode: httpResponseMessage.StatusCode);
            }
            else
            {
                ErrorResponse errorResponse = await Task.Run(
                    () => DeserializeModel<ErrorResponse>(httpResponseMessage, jsonConverters)
                );
                return GetServiceResponse<T>(error: errorResponse, statusCode : httpResponseMessage.StatusCode);
            }
        }

        protected async Task ClearOnlineCacheForUrlsStartingWith<T>(string urlPrefix)
        {
            this.LoggerService.LogDebug(
                LogLevel.DEBUG,
                "Remove online cache for objects from type: {0} with keys starting with: {1}",
                typeof(T).Name,
                urlPrefix
            );
            await this.CacheService.OnlineCache.InvalidateObjectWithKeysStartingWith<T>(urlPrefix);
        }

        protected async Task ClearOnlineCacheForSpecificUrl<T>(string url)
        {
            this.LoggerService.LogDebug(
                LogLevel.DEBUG,
                "Remove online cache for object from type: {0} with key:{1}",
                typeof(T).Name,
                url
            );
            await this.CacheService.OnlineCache.InvalidateObject<T>(url);
        }

        protected async Task ClearOnlineCacheForObjects<T>()
        {
            this.LoggerService.LogDebug(
                LogLevel.DEBUG,
                "Remove online cache for objects from type: {0}",
                typeof(T).Name
            );
            await this.CacheService.OnlineCache.InvalidateAllObjects<T>();
        }

        private async Task<Exception> LogException(HttpResponseMessage httpResponseMessage, string url, string content = null)
        {
            var exception = $"{GetType().FullName}\n{url}\n";
            exception += await httpResponseMessage.Content.ReadAsStringAsync() + "\n";
            if (!string.IsNullOrEmpty(content))
            {
                exception += content + "\n";
            }
            var ex = new Exception(exception);
            TrackingService.TrackException(ex);
            return ex;
        }

        /// <summary>
        /// Generate a ServiceResponse for the api consumer
        /// </summary>
        /// <typeparam name="T">Type of ServiceResponse object to create.</typeparam>
        /// <param name="model">The Model object ServiceResponse will wrap with.</param>
        /// <param name="statusCode">HttpStatusCode in the HttpResponseMessage.</param>
        /// <param name="error">error in HttpResponseMessage.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="isCached">is the response from cache.</param>
        /// <returns>ServiceResponse of type T</returns>
        /// GetServiceResponse
        public ServiceResponse<T> GetServiceResponse<T>(
            T model = null,
            HttpStatusCode statusCode = (HttpStatusCode)9999,
            ErrorResponse error = null,
            Exception exception = null,
            bool isCached = false
        ) where T : class
        {
            if (statusCode != (HttpStatusCode)9999)
            {
                return new ServiceResponse<T>
                {
                    Model = model,
                    Error = error,
                    Exception = exception,
                    StatusCode = statusCode,
                    IsCached = isCached
                };
            }
            else
            {
                return new ServiceResponse<T>
                {
                    Model = model,
                    Error = error,
                    Exception = exception,
                    IsCached = isCached
                };
            }
        }
    }
}
