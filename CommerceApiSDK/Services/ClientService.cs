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

namespace CommerceApiSDK.Services
{
    // Uncomment this row in order to be able to inspect web traffic with Charles app.
    // using CoreFoundation;

    /// <summary>
    /// A REST client for communication with Insite Commerce API.
    /// </summary>
    public class ClientService : IClientService
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

        public ClientService(ISecureStorageService secureStorageService, ILocalStorageService localStorageService, IMvxMessenger messenger, ITrackingService trackingService)
        {
            this.secureStorageService = secureStorageService;
            this.localStorageService = localStorageService;
            this.messenger = messenger;
            this.trackingService = trackingService;
            CreateClient();
        }

        public void CreateClient()
        {
            using (new LogTimer("CreateClient"))
            {
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

                client = new HttpClient(new RefreshTokenHandler(httpClientHandler, RenewAuthenticationTokens, NotifyRefreshTokenExpired))
                {
                    Timeout = Timeout.InfiniteTimeSpan,
                };
                client.DefaultRequestHeaders.Add("User-Agent", "insitemobileapp");
            }
        }

        public virtual async Task<HttpResponseMessage> GetAsync(string path, TimeSpan? timeout = null, CancellationToken? cancellationToken = null)
        {
            Logger.LogDebug("Sending GetAsync {0}", path);
            HttpResponseMessage response;
            using (new LogTimer($"GetAsync {path}"))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, MakeUrl(path)))
                {
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                    Logger.LogTrace("{0} Response {1}", path, response);
                }
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
            using (new LogTimer($"GetAsyncNoHost  {path}"))
            {
                response = await client.GetAsync(path, cancellationToken);
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
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
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
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, MakeUrl(path)))
                {
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
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
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
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
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, MakeUrl(path)))
                {
                    request.Content = content;
                    request.SetTimeout(timeout);
                    response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                }
            }

            Logger.LogDebug("PutAsync {0} finished with status: {1} ", path, response.StatusCode);

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
                string token = secureStorageService.Load(BearerTokenStorageKey);
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
            using (new LogTimer("RefreshTokens"))
            {
                client.DefaultRequestHeaders.Authorization = null;

                string refreshToken = secureStorageService.Load(RefreshTokenStorageKey);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Protocol}{Host}/{TokenUri}");
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
                Logger.LogDebug("RefershToken PostAsync {0} finished with status: {1} ", TokenUri, response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                TokenResult token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(response));
                StoreAccessToken(token);
                SetBearerAuthorizationHeader(token.AccessToken);
                StoreSessionState();
            }

            return true;
        }

        protected virtual void NotifyRefreshTokenExpired()
        {
            messenger.Publish(new RefreshTokenExpiredMessage(this));
        }

        public void Reset()
        {
            StoreCookies();
            CreateClient();
            Logger.LogDebug("The ClientService was reset.");
        }

        public void StoreSessionState(Session currentSession = null)
        {
            StoreCookies(currentSession);
            Logger.LogDebug("State was stored.");
        }

        private void StoreCookies(Session currentSession = null)
        {
            Logger.LogDebug("Saving cookies");
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

                localStorageService.Save(CookiesStorageKey, cookieValues);
            }
        }

        public void LoadSessionState()
        {
            Logger.LogDebug("Loading state");
            string accessToken = secureStorageService.Load(BearerTokenStorageKey);
            if (!string.IsNullOrEmpty(accessToken))
            {
                SetBearerAuthorizationHeader(accessToken);
            }

            LoadCookies();
            Logger.LogDebug("Loaded state");
        }

        public void SetCookie(Cookie cookie)
        {
            httpClientHandler.CookieContainer.Add(Url, cookie);
        }

        protected void LoadCookies()
        {
            Logger.LogDebug("Loading Cookies");
            string cookieValues = localStorageService.Load(CookiesStorageKey, string.Empty);
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

            Logger.LogDebug("Loaded Cookies");
        }

        public void SetBasicAuthorizationHeader()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode($"{ClientId}:{ClientSecret}"));
        }

        public void SetBearerAuthorizationHeader(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                TimeSpan timeout = request.GetTimeout();
                if (timeout == Timeout.InfiniteTimeSpan)
                {
                    // No need to create a CTS if there's no timeout
                    return null;
                }
                else
                {
                    CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
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
                    using (CancellationTokenSource cts = GetCancellationTokenSource(request, cancellationToken))
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
                            lock (refreshingTokenLock)
                            {
                                bool success = renewAuthenticationTokensCallback().Result;
                                if (!success)
                                {
                                    refreshTokenExpiredNotificationCallback?.Invoke();
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
            return !string.IsNullOrEmpty(secureStorageService.Load(BearerTokenStorageKey));
        }

        public void StoreAccessToken(TokenResult tokens)
        {
            secureStorageService.Save(BearerTokenStorageKey, tokens.AccessToken);
            secureStorageService.Save(RefreshTokenStorageKey, tokens.RefreshToken);

            TimeSpan timeSpan = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn).TimeOfDay;
            secureStorageService.Save(ExpiresInStorageKey, timeSpan.TotalMilliseconds.ToString());
        }

        public void RemoveAccessToken()
        {
            secureStorageService.Remove(BearerTokenStorageKey);
            secureStorageService.Remove(RefreshTokenStorageKey);
            secureStorageService.Remove(ExpiresInStorageKey);
        }

        public async Task<string> GetAccessToken()
        {
            string timestampStr = secureStorageService.Load(ExpiresInStorageKey);
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

            return secureStorageService.Load(BearerTokenStorageKey);
        }

        public async Task<ServiceResponse<TokenResult>> Generate(string userName, string password)
        {
            using (new LogTimer("GetToken"))
            {
                SetBasicAuthorizationHeader();

                FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("scope", $"{ApiScopeKey} offline_access"),
                    });

                HttpResponseMessage result = await PostAsync(TokenUri, requestContent);
                if (!result.IsSuccessStatusCode)
                {
                    ErrorResponse error = await Task.Run(() => ServiceBase.DeserializeModel<ErrorResponse>(result));
                    return new ServiceResponse<TokenResult> { Error = error };
                }

                TokenResult token = await Task.Run(() => ServiceBase.DeserializeModel<TokenResult>(result));
                StoreAccessToken(token);
                return new ServiceResponse<TokenResult> { Model = token };
            }
        }
        #endregion
    }
}