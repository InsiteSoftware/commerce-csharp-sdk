namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using CommerceApiSDK.Extensions;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Services.Messages;
    using CommerceApiSDK.Utils.Logger;
    using MvvmCross.Plugin.Messenger;

    // Uncomment this row in order to be able to inspect web traffic with Charles app.
    // using CoreFoundation;

    /// <summary>
    /// A REST client for communication with Insite Commerce API.
    /// </summary>
    public class IscClientService : IClientService
    {
        protected const string TokenUri = "identity/connect/token";
        private const string TokenLogoutUri = "identity/connect/endsession";
        private const string TokenValidationUri = "identity/connect/accesstokenvalidation?token=";
        protected virtual string ClientId { get; } = "mobile";
        protected virtual string ClientSecret { get; } = "009AC476-B28E-4E33-8BAE-B5F103A142BC";

        protected virtual string BearerTokenStorageKey { get; } = "bearerToken";
        protected virtual string RefreshTokenStorageKey { get; } = "refreshToken";
        protected virtual string ExpiresInStorageKey { get; } = "expiresIn";
        protected virtual string ApiScopeKey { get; } = "iscapi";
        protected virtual string CookiesStorageKey { get; } = "cookies";

        protected HttpClient client;
        private HttpClientHandler httpClientHandler;

        private readonly ISecureStorageService secureStorageService;
        private readonly ILocalStorageService localStorageService;
        protected readonly IMvxMessenger messenger;
        protected readonly ITrackingService trackingService;

        protected virtual string[] StoredCookiesNames { get; } = { "CurrentPickUpWarehouseId", "CurrentFulfillmentMethod", "CurrentBillToId", "CurrentShipToId", "BillToIdShipToId", "CurrentLanguageId", "SetContextLanguageCode", "SetContextPersonaIds" };

        public bool IsSecure { get; set; } = true;

        private string host;

        public string Host
        {
            get => this.host;
            set
            {
                if (value != this.host)
                {
                    this.host = value;
                    if (!string.IsNullOrEmpty(this.host))
                    {
                        this.LoadSessionState();
                    }
                }
            }
        }

        public Uri Url => new Uri($"{this.Protocol}{this.Host}");

        protected CookieCollection Cookies => this.httpClientHandler?.CookieContainer?.GetCookies(this.Url);

        protected string Protocol => this.IsSecure ? "https://" : "http://";

        public string SessionStateKey
        {
            get
            {
                var result = "+cookies:";
                if (this.Cookies != null)
                {
                    foreach (var storedCookieName in this.StoredCookiesNames)
                    {
                        foreach (Cookie cookie in this.Cookies)
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

        public IscClientService(ISecureStorageService secureStorageService, ILocalStorageService localStorageService, IMvxMessenger messenger, ITrackingService trackingService)
        {
            this.secureStorageService = secureStorageService;
            this.localStorageService = localStorageService;
            this.messenger = messenger;
            this.trackingService = trackingService;
            this.CreateClient();
        }

        public void CreateClient()
        {
            using (new LogTimer("CreateClient"))
            {
                this.httpClientHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = new CookieContainer(),
                    ClientCertificateOptions = ClientCertificateOption.Automatic,

                    // Uncomment these two rows in order to be able to inspect web traffic with Charles app.
                    // UseProxy = true,
                    // Proxy = CFNetwork.GetDefaultProxy()
                };

                this.client = new HttpClient(new RefreshTokenHandler(this.httpClientHandler, this.RenewAuthenticationTokens, this.NotifyRefreshTokenExpired))
                {
                    Timeout = Timeout.InfiniteTimeSpan,
                };
                this.client.DefaultRequestHeaders.Add("User-Agent", "insitemobileapp");
            }
        }

        public virtual async Task<HttpResponseMessage> GetAsync(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            Logger.LogDebug("Sending GetAsync {0}", path);
            HttpResponseMessage response;
            using (new LogTimer($"GetAsync {path}"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, this.MakeUrl(path)))
                {
                    request.SetTimeout(timeout);
                    response = await this.SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                    Logger.LogTrace("{0} Response {1}", path, response);
                }
            }

            return response;
        }

        public virtual async Task<HttpResponseMessage> GetAsyncNoHost(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                request.SetTimeout(timeout);
            }

            HttpResponseMessage response;
            using (new LogTimer($"GetAsyncNoHost  {path}"))
            {
                response = await this.client.GetAsync(path, cancellationToken);
            }

            Logger.LogDebug("GET async no host {0} finished with status: {1} ", path, response.StatusCode);
            Logger.LogTrace("{0} Response {1}", path, response);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PostAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            Logger.LogTrace("Posting Async content for {0} : {1}", path, content);
            HttpResponseMessage response;
            using (new LogTimer($"PostAsync {path}"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, this.MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await this.SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }
            }

            Logger.LogDebug("PostAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage response;
            using (new LogTimer($"DeleteAsync {path}"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Delete, this.MakeUrl(path)))
                {
                    request.SetTimeout(timeout);
                    response = await this.SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }
            }

            Logger.LogTrace("DeleteAsync Response for {0} : {1}", path, response);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PatchAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            Logger.LogTrace("Patching Async content for {0} : {1}", path, content);
            HttpResponseMessage response;
            using (new LogTimer($"PatchAsync {path}"))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), this.MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await this.SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }
            }

            Logger.LogDebug("PatchAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PutAsync(string path, HttpContent content, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            HttpResponseMessage response;
            using (new LogTimer($"PutAsync {path}"))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Put, this.MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await this.SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }
            }

            Logger.LogDebug("PutAsync {0} finished with status: {1} ", path, response.StatusCode);

            return response;
        }

        private async Task<HttpResponseMessage> SendRequestUpToTwiceIfNeededAsync(HttpRequestMessage requestMessage, CancellationToken? cancellationToken = null)
        {
            var response = cancellationToken.HasValue
                            ? await this.client.SendAsync(requestMessage, cancellationToken.Value)
                            : await this.client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // If token is null/empty after Forbidden status, we aren't logged in
                // so no need to retry
                var token = this.secureStorageService.Load(this.BearerTokenStorageKey);
                if (string.IsNullOrEmpty(token))
                {
                    return response;
                }

                // token refreshed
                cancellationToken?.ThrowIfCancellationRequested();

                using (var newRequestMessage = new HttpRequestMessage(requestMessage.Method, requestMessage.RequestUri))
                {
                    newRequestMessage.SetTimeout(requestMessage.GetTimeout());
                    newRequestMessage.Content = requestMessage.Content;

                    response = cancellationToken.HasValue
                                ? await this.client.SendAsync(newRequestMessage, cancellationToken.Value)
                                : await this.client.SendAsync(newRequestMessage);
                }
            }

            return response;
        }

        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
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

            if (this.Host.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                return $"{this.Host}/{path}";
            }
            else
            {
                return $"{this.Protocol}{this.Host}/{path}";
            }
        }

        public async Task<bool> RenewAuthenticationTokens()
        {
            using (new LogTimer("RefreshTokens"))
            {
                this.client.DefaultRequestHeaders.Authorization = null;

                var refreshToken = this.secureStorageService.Load(this.RefreshTokenStorageKey);
                var request = new HttpRequestMessage(HttpMethod.Post, $"{this.Protocol}{this.Host}/{TokenUri}");
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", refreshToken),
                        new KeyValuePair<string, string>("client_id", this.ClientId),
                        new KeyValuePair<string, string>("client_secret", this.ClientSecret),
                });
                request.Content = content;
                request.SetTimeout(request.GetTimeout());
                var response = await this.client.SendAsync(request);
                Logger.LogDebug("RefershToken PostAsync {0} finished with status: {1} ", TokenUri, response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(response));
                this.StoreAccessToken(token);
                this.SetBearerAuthorizationHeader(token.AccessToken);
                this.StoreSessionState();
            }

            return true;
        }

        protected virtual void NotifyRefreshTokenExpired()
        {
            this.messenger.Publish(new RefreshTokenExpiredMessage(this));
        }

        public void Reset()
        {
            this.StoreCookies();
            this.CreateClient();
            Logger.LogDebug("The ClientService was reset.");
        }

        public void StoreSessionState(Session currentSession = null)
        {
            this.StoreCookies(currentSession);
            Logger.LogDebug("State was stored.");
        }

        private void StoreCookies(Session currentSession = null)
        {
            Logger.LogDebug("Saving cookies");
            if (this.Cookies != null)
            {
                var cookieValues = string.Empty;
                foreach (Cookie cookie in this.Cookies)
                {
                    if (this.StoredCookiesNames.Contains(cookie.Name))
                    {
                        if (currentSession != null && cookie.Name.Equals("CurrentShipToId") && currentSession.ShipTo != null)
                        {
                            cookie.Value = currentSession.ShipTo.Id; // fixes weird issue when the ShipTo cookie is not updated when you choose the first ShipTo in the list
                        }

                        cookieValues += $"{cookie.Name}={cookie.Value}|";
                    }
                }

                this.localStorageService.Save(this.CookiesStorageKey, cookieValues);
            }
        }

        public void LoadSessionState()
        {
            Logger.LogDebug("Loading state");
            var accessToken = this.secureStorageService.Load(this.BearerTokenStorageKey);
            if (!string.IsNullOrEmpty(accessToken))
            {
                this.SetBearerAuthorizationHeader(accessToken);
            }

            this.LoadCookies();
            Logger.LogDebug("Loaded state");
        }

        public void SetCookie(Cookie cookie)
        {
            this.httpClientHandler.CookieContainer.Add(this.Url, cookie);
        }

        protected void LoadCookies()
        {
            Logger.LogDebug("Loading Cookies");
            var cookieValues = this.localStorageService.Load(this.CookiesStorageKey, string.Empty);
            if (string.IsNullOrEmpty(cookieValues))
            {
                return;
            }

            foreach (var cookieValue in cookieValues.Split('|'))
            {
                if (cookieValue.Contains("="))
                {
                    var name = cookieValue.Split('=')[0];
                    var value = cookieValue.Split('=')[1];

                    this.httpClientHandler.CookieContainer.Add(this.Url, new Cookie(name, value));
                }
            }

            Logger.LogDebug("Loaded Cookies");
        }

        public void SetBasicAuthorizationHeader()
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.Base64Encode($"{this.ClientId}:{this.ClientSecret}"));
        }

        public void SetBearerAuthorizationHeader(string token)
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private class RefreshTokenHandler : DelegatingHandler
        {
            private readonly Func<Task<bool>> renewAuthenticationTokensCallback;
            private readonly object refreshingTokenLock = new object();

            private readonly Action refreshTokenExpiredNotificationCallback;

            public RefreshTokenHandler(
                HttpMessageHandler messageHandler,
                Func<Task<bool>> renewAuthenticationTokensCallback,
                Action refreshTokenExpiredNotificationCallback) : base(messageHandler)
            {
                this.renewAuthenticationTokensCallback = renewAuthenticationTokensCallback;
                this.refreshTokenExpiredNotificationCallback = refreshTokenExpiredNotificationCallback;
            }

            private CancellationTokenSource GetCancellationTokenSource(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var timeout = request.GetTimeout();
                if (timeout == Timeout.InfiniteTimeSpan)
                {
                    // No need to create a CTS if there's no timeout
                    return null;
                }
                else
                {
                    var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(timeout);
                    return cts;
                }
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Logger.LogTrace("Sending Async request : {0} {1}", request, cancellationToken);
                HttpResponseMessage result = null;
                using (new LogTimer($"SendAsync {request?.RequestUri}"))
                {
                    using (var cts = this.GetCancellationTokenSource(request, cancellationToken))
                    {
                        try
                        {
                            result = await base.SendAsync(request, cts?.Token ?? cancellationToken);
                            Logger.LogTrace("SendAsync Response : {0}", result);
                        }
                        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
                        {
                            throw new TimeoutException();
                        }
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        return result;
                    }

                    if (request.RequestUri.AbsolutePath.Contains(TokenUri) || request.RequestUri.AbsolutePath.Contains(TokenLogoutUri))
                    {
                        return result;
                    }

                    if (result.StatusCode == HttpStatusCode.Unauthorized || result.StatusCode == HttpStatusCode.Forbidden)
                    {
                        await Task.Run(() =>
                        {
                            lock (this.refreshingTokenLock)
                            {
                                var success = this.renewAuthenticationTokensCallback().Result;
                                if (!success)
                                {
                                    this.refreshTokenExpiredNotificationCallback?.Invoke();
                                }
                            }
                        });
                    }
                }

                return result;
            }
        }

        #region Access Token

        public bool IsExistsAccessToken()
        {
            return !string.IsNullOrEmpty(this.secureStorageService.Load(this.BearerTokenStorageKey));
        }

        public void StoreAccessToken(TokenResult tokens)
        {
            this.secureStorageService.Save(this.BearerTokenStorageKey, tokens.AccessToken);
            this.secureStorageService.Save(this.RefreshTokenStorageKey, tokens.RefreshToken);

            var timeSpan = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn).TimeOfDay;
            this.secureStorageService.Save(this.ExpiresInStorageKey, timeSpan.TotalMilliseconds.ToString());
        }

        public void RemoveAccessToken()
        {
            this.secureStorageService.Remove(this.BearerTokenStorageKey);
            this.secureStorageService.Remove(this.RefreshTokenStorageKey);
            this.secureStorageService.Remove(this.ExpiresInStorageKey);
        }

        public async Task<string> GetAccessToken()
        {
            string timestampStr = this.secureStorageService.Load(this.ExpiresInStorageKey);
            if (!string.IsNullOrEmpty(timestampStr))
            {
                var timestamp = double.Parse(timestampStr);
                if (timestamp < DateTime.UtcNow.TimeOfDay.TotalMilliseconds)
                {
                    var result = await this.RenewAuthenticationTokens();
                    if (!result)
                    {
                        return string.Empty;
                    }
                }
            }

            return this.secureStorageService.Load(this.BearerTokenStorageKey);
        }

        public async Task<ServiceResponse<TokenResult>> Generate(string userName, string password)
        {
            using (new LogTimer("GetToken"))
            {
                this.SetBasicAuthorizationHeader();

                var requestContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("scope", $"{this.ApiScopeKey} offline_access"),
                    });

                var result = await this.PostAsync(TokenUri, requestContent);
                if (!result.IsSuccessStatusCode)
                {
                    var error = await Task.Run(() => ServiceBase.DeserializeModel<ErrorResponse>(result));
                    return new ServiceResponse<TokenResult> { Error = error };
                }

                var token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(result));
                this.StoreAccessToken(token);
                return new ServiceResponse<TokenResult> { Model = token };
            }
        }
        #endregion
    }
}