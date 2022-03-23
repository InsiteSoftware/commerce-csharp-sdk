using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Extensions;
using CommerceApiSDK.Handler;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services
{
    // Uncomment this row in order to be able to inspect web traffic with Charles app.
    // using CoreFoundation;

    /// <summary>
    /// A REST client for communication with Insite Commerce API.
    /// </summary>
    public class ClientService : IClientService
    {
        protected virtual string ClientId { get; } = ClientConfig.ClientId;
        protected virtual string ClientSecret { get; } = ClientConfig.ClientSecret;

        protected virtual string BearerTokenStorageKey { get; } = "bearerToken";
        protected virtual string RefreshTokenStorageKey { get; } = "refreshToken";
        protected virtual string ExpiresInStorageKey { get; } = "expiresIn";
        protected virtual string ApiScopeKey { get; } = "iscapi";
        protected virtual string CookiesStorageKey { get; } = "cookies";

        protected HttpClient client;
        public HttpClientHandler httpClientHandler;

        protected IOptiAPIBaseServiceProvider _optiAPIBaseServiceProvider;

        protected virtual string[] StoredCookiesNames { get; } = { "CurrentPickUpWarehouseId", "CurrentFulfillmentMethod", "CurrentBillToId", "CurrentShipToId", "BillToIdShipToId", "CurrentLanguageId", "SetContextLanguageCode", "SetContextPersonaIds" };

        public bool IsSecure { get; set; } = true;

        private string host;

        public string Host
        {
            get => host;
            set
            {
                if (value != host)
                {
                    host = value;
                    if (!string.IsNullOrEmpty(host))
                    {
                        LoadSessionState();
                    }
                }
            }
        }

        public Uri Url => new Uri($"{Protocol}{Host}");

        protected CookieCollection Cookies => httpClientHandler?.CookieContainer?.GetCookies(Url);

        protected string Protocol => IsSecure ? "https://" : "http://";

        public string SessionStateKey
        {
            get
            {
                string result = "+cookies:";
                if (Cookies != null)
                {
                    foreach (string storedCookieName in StoredCookiesNames)
                    {
                        foreach (Cookie cookie in Cookies)
                        {
                            if (cookie.Name.Equals(storedCookieName))
                            {
                                result += $"{cookie.Name}={cookie.Value}|";
                                break;
                            }
                        }
                    }
                }

                return result;
            }
        }

        public string ErrorMessage { get; set; }

        public ClientService(IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
        {
            _optiAPIBaseServiceProvider = optiAPIBaseServiceProvider;
            CreateClient();
        }

        public void CreateClient()
        {
            Host = ClientConfig.HostUrl;

            httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                ClientCertificateOptions = ClientCertificateOption.Automatic,

                // Uncomment these two rows in order to be able to inspect web traffic with Charles app.
                // UseProxy = true,
                // Proxy = CFNetwork.GetDefaultProxy()
            };

            client = new HttpClient(new RefreshTokenHandler(httpClientHandler, RenewAuthenticationTokens, _optiAPIBaseServiceProvider.GetLoggerService(), NotifyRefreshTokenExpired))
            {
                Timeout = Timeout.InfiniteTimeSpan,
            };
        }

        public virtual async Task<HttpResponseMessage> GetAsync(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Sending GetAsync {0}", path);
            HttpResponseMessage response;
            
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, MakeUrl(path)))
                {
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "{0} Response {1}", path, response);
                }

            return response;
        }

        public virtual async Task<HttpResponseMessage> GetAsyncNoHost(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                request.SetTimeout(timeout);
            }

            HttpResponseMessage response;
    
            response = await client.GetAsync(path, cancellationToken);

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "GET async no host {0} finished with status: {1} ", path, response.StatusCode);
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "{0} Response {1}", path, response);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PostAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "Posting Async content for {0} : {1}", path, content);
            HttpResponseMessage response;
            
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "PostAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage response;
           
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, MakeUrl(path)))
                {   
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }

            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "DeleteAsync Response for {0} : {1}", path, response);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PatchAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "Patching Async content for {0} : {1}", path, content);
            HttpResponseMessage response;
            
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "PatchAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PutAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage response;
            
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "PutAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        private async Task<HttpResponseMessage> SendRequestUpToTwiceIfNeededAsync(HttpRequestMessage requestMessage, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage response = cancellationToken.HasValue
                            ? await client.SendAsync(requestMessage, cancellationToken.Value)
                            : await client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // If token is null/empty after Forbidden status, we aren't logged in
                // so no need to retry
                string token = _optiAPIBaseServiceProvider.GetSecureStorageService().Load(BearerTokenStorageKey);
                if (string.IsNullOrEmpty(token))
                {
                    return response;
                }

                // token refreshed
                cancellationToken?.ThrowIfCancellationRequested();

                using (HttpRequestMessage newRequestMessage = new HttpRequestMessage(requestMessage.Method, requestMessage.RequestUri))
                {
                    newRequestMessage.SetTimeout(requestMessage.GetTimeout());
                    newRequestMessage.Content = requestMessage.Content;

                    response = cancellationToken.HasValue
                                ? await client.SendAsync(newRequestMessage, cancellationToken.Value)
                                : await client.SendAsync(newRequestMessage);
                }
            }

            return response;
        }

        protected string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string MakeUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            if (Host.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                return $"{Host}/{path}";
            }
            else
            {
                return $"{Protocol}{Host}/{path}";
            }
        }

        public async Task<bool> RenewAuthenticationTokens()
        {
           
                client.DefaultRequestHeaders.Authorization = null;

                string refreshToken = _optiAPIBaseServiceProvider.GetSecureStorageService().Load(RefreshTokenStorageKey);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Protocol}{Host}/{CommerceAPIConstants.TokenUri}");
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", refreshToken),
                        new KeyValuePair<string, string>("client_id", ClientId),
                        new KeyValuePair<string, string>("client_secret", ClientSecret),
                });
                request.Content = content;
                request.SetTimeout(request.GetTimeout());
                HttpResponseMessage response = await client.SendAsync(request);

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "RefershToken PostAsync {0} finished with status: {1} ", CommerceAPIConstants.TokenUri, response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                TokenResult token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(response));
                StoreAccessToken(token);
                SetBearerAuthorizationHeader(token.AccessToken);
                StoreSessionState();

            return true;
        }

        protected virtual void NotifyRefreshTokenExpired()
        {
            _optiAPIBaseServiceProvider.GetMessengerService().Publish(new RefreshTokenExpiredOptiMessage());
        }

        public void Reset()
        {
            StoreCookies();
            CreateClient();

            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "The ClientService was reset.");
        }

        public void StoreSessionState(Session currentSession = null)
        {
            StoreCookies(currentSession);
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "State was stored.");
        }

        private void StoreCookies(Session currentSession = null)
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Saving cookies");
            if (Cookies != null)
            {
                string cookieValues = string.Empty;
                foreach (Cookie cookie in Cookies)
                {
                    if (StoredCookiesNames.Contains(cookie.Name))
                    {
                        if (currentSession != null && cookie.Name.Equals("CurrentShipToId") && currentSession.ShipTo != null)
                        {
                            cookie.Value = currentSession.ShipTo.Id; // fixes weird issue when the ShipTo cookie is not updated when you choose the first ShipTo in the list
                        }

                        cookieValues += $"{cookie.Name}={cookie.Value}|";
                    }
                }

                _optiAPIBaseServiceProvider.GetLocalStorageService().Save(CookiesStorageKey, cookieValues);
            }
        }

        public void LoadSessionState()
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Loading state");
            string accessToken = _optiAPIBaseServiceProvider.GetSecureStorageService().Load(BearerTokenStorageKey);
            if (!string.IsNullOrEmpty(accessToken))
            {
                SetBearerAuthorizationHeader(accessToken);
            }

            LoadCookies();
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Loaded state");
        }

        public void SetCookie(Cookie cookie)
        {
            httpClientHandler.CookieContainer.Add(Url, cookie);
        }

        protected void LoadCookies()
        {
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Loading Cookies");
            string cookieValues = _optiAPIBaseServiceProvider.GetLocalStorageService().Load(CookiesStorageKey, string.Empty);
            if (string.IsNullOrEmpty(cookieValues))
            {
                return;
            }

            foreach (string cookieValue in cookieValues.Split('|'))
            {
                if (cookieValue.Contains("="))
                {
                    string name = cookieValue.Split('=')[0];
                    string value = cookieValue.Split('=')[1];

                    httpClientHandler.CookieContainer.Add(Url, new Cookie(name, value));
                }
            }
            _optiAPIBaseServiceProvider.GetLoggerService().LogDebug(LogLevel.DEBUG, "Loaded Cookies");
        }

        public void SetBasicAuthorizationHeader()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode($"{ClientId}:{ClientSecret}"));
        }

        public void SetBearerAuthorizationHeader(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

      

        #region Access Token

        public bool IsExistsAccessToken()
        {
            return !string.IsNullOrEmpty(_optiAPIBaseServiceProvider.GetSecureStorageService().Load(BearerTokenStorageKey));
        }

        public void StoreAccessToken(TokenResult tokens)
        {
            _optiAPIBaseServiceProvider.GetSecureStorageService().Save(BearerTokenStorageKey, tokens.AccessToken);
            _optiAPIBaseServiceProvider.GetSecureStorageService().Save(RefreshTokenStorageKey, tokens.RefreshToken);

            TimeSpan timeSpan = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn).TimeOfDay;
            _optiAPIBaseServiceProvider.GetSecureStorageService().Save(ExpiresInStorageKey, timeSpan.TotalMilliseconds.ToString());
        }

        public void RemoveAccessToken()
        {
            _optiAPIBaseServiceProvider.GetSecureStorageService().Remove(BearerTokenStorageKey);
            _optiAPIBaseServiceProvider.GetSecureStorageService().Remove(RefreshTokenStorageKey);
            _optiAPIBaseServiceProvider.GetSecureStorageService().Remove(ExpiresInStorageKey);
        }

        public async Task<string> GetAccessToken()
        {
            string timestampStr = _optiAPIBaseServiceProvider.GetSecureStorageService().Load(ExpiresInStorageKey);
            if (!string.IsNullOrEmpty(timestampStr))
            {
                double timestamp = double.Parse(timestampStr);
                if (timestamp < DateTime.UtcNow.TimeOfDay.TotalMilliseconds)
                {
                    bool result = await RenewAuthenticationTokens();
                    if (!result)
                    {
                        return string.Empty;
                    }
                }
            }

            return _optiAPIBaseServiceProvider.GetSecureStorageService().Load(BearerTokenStorageKey);
        }

        public async Task<ServiceResponse<TokenResult>> Generate(string userName, string password)
        {
           
                SetBasicAuthorizationHeader();

                FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("scope", $"{ApiScopeKey} offline_access"),
                    });

                HttpResponseMessage result = await PostAsync(CommerceAPIConstants.TokenUri, requestContent);
                if (!result.IsSuccessStatusCode)
                {
                    ErrorResponse error = await Task.Run(() => ServiceBase.DeserializeModel<ErrorResponse>(result));
                    return new ServiceResponse<TokenResult> { Error = error };
                }

                TokenResult token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(result));
                StoreAccessToken(token);
                return new ServiceResponse<TokenResult> { Model = token };
        }
        #endregion
    }
}