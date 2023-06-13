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
        protected virtual string ClientId { get; set; } = ClientConfig.ClientId;
        protected virtual string ClientSecret { get; set; } = ClientConfig.ClientSecret;

        protected virtual string BearerTokenStorageKey { get; } = "bearerToken";
        protected virtual string RefreshTokenStorageKey { get; } = "refreshToken";
        protected virtual string ExpiresInStorageKey { get; } = "expiresIn";
        protected virtual string ApiScopeKey { get; } = "iscapi";
        protected virtual string CookiesStorageKey { get; } = "cookies";

        protected HttpClient client;
        public HttpClientHandler httpClientHandler;

        protected readonly IMessengerService OptiMessenger;
        protected readonly ITrackingService TrackingService;

        private readonly ISecureStorageService secureStorageService;
        private readonly ILocalStorageService localStorageService;
        private readonly ILoggerService loggerService;

        protected virtual string[] StoredCookiesNames { get; } =

            {
                "CurrentPickUpWarehouseId",
                "CurrentFulfillmentMethod",
                "CurrentBillToId",
                "CurrentShipToId",
                "BillToIdShipToId",
                "CurrentLanguageId",
                "SetContextLanguageCode",
                "SetContextPersonaIds"
            };

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

        public ClientService(
            ISecureStorageService secureStorageService,
            ILocalStorageService localStorageService,
            IMessengerService optiMessenger,
            ITrackingService trackingService,
            ILoggerService loggerService
        )
        {
            this.OptiMessenger = optiMessenger;
            this.TrackingService = trackingService;
            this.secureStorageService = secureStorageService;
            this.localStorageService = localStorageService;
            this.loggerService = loggerService;

            CreateClient();
        }

        public void CreateClient()
        {
            httpClientHandler = HttpClientHandler;

            client = new HttpClient(
                new RefreshTokenHandler(
                    httpClientHandler,
                    RenewAuthenticationTokens,
                    this.loggerService,
                    NotifyRefreshTokenExpired
                )
            )
            {
                Timeout = Timeout.InfiniteTimeSpan,
            };

            Host = ClientConfig.HostUrl;
        }

        public virtual HttpClientHandler HttpClientHandler => new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer(),
            ClientCertificateOptions = ClientCertificateOption.Automatic,

            // Uncomment these two rows in order to be able to inspect web traffic with Charles app.
            // UseProxy = true,
            // Proxy = CFNetwork.GetDefaultProxy()
        };

        public HttpClient GetHttpClient()
        {
            if (client == null)
            {
                CreateClient();
            }
            return client;
        }

        public virtual async Task<HttpResponseMessage> GetAsync(
            string path,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            this.loggerService.LogDebug(LogLevel.DEBUG, "Sending GetAsync {0}", path);
            HttpResponseMessage response;

            using (
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, MakeUrl(path))
            )
            {
                request.SetTimeout(timeout);
                response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
                this.loggerService.LogConsole(LogLevel.INFO, "{0} Response {1}", path, response);
            }

            return response;
        }

        public virtual async Task<HttpResponseMessage> GetAsyncNoHost(
            string path,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path))
            {
                request.SetTimeout(timeout);
            }

            HttpResponseMessage response;

            response = await client.GetAsync(path, cancellationToken);

            this.loggerService.LogDebug(
                LogLevel.DEBUG,
                "GET async no host {0} finished with status: {1} ",
                path,
                response?.StatusCode
            );
            this.loggerService.LogConsole(LogLevel.INFO, "{0} Response {1}", path, response);

            return response;
        }

        public virtual async Task<HttpResponseMessage> PostAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            this.loggerService.LogConsole(
                LogLevel.INFO,
                "Posting Async content for {0} : {1}",
                path,
                content
            );
            HttpResponseMessage response;

            using (
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, MakeUrl(path))
            )
            {
                request.Content = content;
                request.SetTimeout(timeout);
                response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
            }

            this.loggerService.LogDebug(
                LogLevel.DEBUG,
                "PostAsync {0} finished with status: {1} ",
                path,
                response.StatusCode
            );

            return response;
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(
            string path,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            HttpResponseMessage response;

            using (
                HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Delete,
                    MakeUrl(path)
                )
            )
            {
                request.SetTimeout(timeout);
                response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
            }

            this.loggerService.LogConsole(
                LogLevel.INFO,
                "DeleteAsync Response for {0} : {1}",
                path,
                response
            );

            return response;
        }

        public virtual async Task<HttpResponseMessage> PatchAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            this.loggerService.LogConsole(
                LogLevel.INFO,
                "Patching Async content for {0} : {1}",
                path,
                content
            );
            HttpResponseMessage response;

            using (
                HttpRequestMessage request = new HttpRequestMessage(
                    new HttpMethod("PATCH"),
                    MakeUrl(path)
                )
            )
            {
                request.Content = content;
                request.SetTimeout(timeout);
                response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
            }

            this.loggerService.LogDebug(
                LogLevel.DEBUG,
                "PatchAsync {0} finished with status: {1} ",
                path,
                response.StatusCode
            );

            return response;
        }

        public virtual async Task<HttpResponseMessage> PutAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        )
        {
            HttpResponseMessage response;

            using (
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, MakeUrl(path))
            )
            {
                request.Content = content;
                request.SetTimeout(timeout);
                response = await SendRequestUpToTwiceIfNeededAsync(request, cancellationToken);
            }

            this.loggerService.LogDebug(
                LogLevel.DEBUG,
                "PutAsync {0} finished with status: {1} ",
                path,
                response?.StatusCode
            );

            return response;
        }

        private async Task<HttpResponseMessage> SendRequestUpToTwiceIfNeededAsync(
            HttpRequestMessage requestMessage,
            CancellationToken? cancellationToken = null
        )
        {
            HttpResponseMessage response = cancellationToken.HasValue
                ? await client.SendAsync(requestMessage, cancellationToken.Value)
                : await client.SendAsync(requestMessage);

            if (
                response.StatusCode == HttpStatusCode.Forbidden
                || response.StatusCode == HttpStatusCode.Unauthorized
            )
            {
                // If token is null/empty after Forbidden status, we aren't logged in
                // so no need to retry
                string token = this.secureStorageService.Load(BearerTokenStorageKey);
                if (string.IsNullOrEmpty(token))
                {
                    return response;
                }

                // token refreshed
                cancellationToken?.ThrowIfCancellationRequested();

                using (
                    HttpRequestMessage newRequestMessage = new HttpRequestMessage(
                        requestMessage.Method,
                        requestMessage.RequestUri
                    )
                )
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

            string refreshToken = this.secureStorageService.Load(RefreshTokenStorageKey);
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{Protocol}{Host}/{CommerceAPIConstants.TokenUrl}"
            );
            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("client_secret", ClientSecret),
                }
            );
            request.Content = content;
            request.SetTimeout(request.GetTimeout());
            HttpResponseMessage response = await client.SendAsync(request);

            this.loggerService.LogDebug(
                LogLevel.DEBUG,
                "RefershToken PostAsync {0} finished with status: {1} ",
                CommerceAPIConstants.TokenUrl,
                response.StatusCode
            );

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            TokenResult token = await Task.Run(
                () => ServiceBase.DeserializeModel<TokenResult>(response)
            );
            StoreAccessToken(token);
            SetBearerAuthorizationHeader(token.AccessToken);
            StoreSessionState();

            return true;
        }

        protected virtual void NotifyRefreshTokenExpired()
        {
            this.OptiMessenger.Publish(new RefreshTokenExpiredOptiMessage());
        }

        public void Reset()
        {
            StoreCookies();
            CreateClient();

            this.loggerService.LogDebug(LogLevel.DEBUG, "The ClientService was reset.");
        }

        public void StoreSessionState(Session currentSession = null)
        {
            StoreCookies(currentSession);
            this.loggerService.LogDebug(LogLevel.DEBUG, "State was stored.");
        }

        private void StoreCookies(Session currentSession = null)
        {
            this.loggerService.LogDebug(LogLevel.DEBUG, "Saving cookies");
            if (Cookies != null)
            {
                string cookieValues = string.Empty;
                foreach (Cookie cookie in Cookies)
                {
                    if (StoredCookiesNames.Contains(cookie.Name))
                    {
                        if (
                            currentSession != null
                            && cookie.Name.Equals("CurrentShipToId")
                            && currentSession.ShipTo != null
                        )
                        {
                            cookie.Value = currentSession.ShipTo.Id; // fixes weird issue when the ShipTo cookie is not updated when you choose the first ShipTo in the list
                        }

                        cookieValues += $"{cookie.Name}={cookie.Value}|";
                    }
                }

                this.localStorageService.Save(CookiesStorageKey, cookieValues);
            }
        }

        public void LoadSessionState()
        {
            this.loggerService.LogDebug(LogLevel.DEBUG, "Loading state");
            string accessToken = this.secureStorageService.Load(BearerTokenStorageKey);
            if (!string.IsNullOrEmpty(accessToken))
            {
                SetBearerAuthorizationHeader(accessToken);
            }

            LoadCookies();
            this.loggerService.LogDebug(LogLevel.DEBUG, "Loaded state");
        }

        public void SetCookie(Cookie cookie)
        {
            httpClientHandler.CookieContainer.Add(Url, cookie);
        }

        protected void LoadCookies()
        {
            this.loggerService.LogDebug(LogLevel.DEBUG, "Loading Cookies");
            string cookieValues = this.localStorageService.Load(CookiesStorageKey, string.Empty);
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
            this.loggerService.LogDebug(LogLevel.DEBUG, "Loaded Cookies");
        }

        public void SetBasicAuthorizationHeader()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Base64Encode($"{ClientId}:{ClientSecret}")
            );
        }

        public void SetBearerAuthorizationHeader(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
        }

        #region Access Token
        public bool IsExistsAccessToken()
        {
            return !string.IsNullOrEmpty(this.secureStorageService.Load(BearerTokenStorageKey));
        }

        public async Task RemoveAlternateCartCookie()
        {
            await Task.Run(() => {
                foreach (Cookie co in Cookies)
                {
                    if (co.Name.Contains("AlternateCart"))
                    {
                        co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                        break;
                    }
                }
            });
        }

        public async Task RemoveOrderApprovalCookieIfAvailable()
        {
            await Task.Run(() => {
                foreach (Cookie co in Cookies)
                {
                    if (co.Name.Contains("CustomerOrderForApproval"))
                    {
                        co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                        break;
                    }
                }
            });
        }

        public async Task<bool> IsCustomerOrderApproval() => await Task.Run(() =>
        {
            foreach (Cookie co in Cookies)
            {
                if (co.Name.Contains("CustomerOrderForApproval"))
                {
                    return true;
                }
            }
            return false;
        });

        public void StoreAccessToken(TokenResult tokens)
        {
            this.secureStorageService.Save(BearerTokenStorageKey, tokens.AccessToken);
            this.secureStorageService.Save(RefreshTokenStorageKey, tokens.RefreshToken);

            TimeSpan timeSpan = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn).TimeOfDay;
            this.secureStorageService.Save(
                ExpiresInStorageKey,
                timeSpan.TotalMilliseconds.ToString()
            );
        }

        public void RemoveAccessToken()
        {
            this.secureStorageService.Remove(BearerTokenStorageKey);
            this.secureStorageService.Remove(RefreshTokenStorageKey);
            this.secureStorageService.Remove(ExpiresInStorageKey);
        }

        public async Task<string> GetAccessToken()
        {
            string timestampStr = this.secureStorageService.Load(ExpiresInStorageKey);
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

            return this.secureStorageService.Load(BearerTokenStorageKey);
        }

        public async Task<ServiceResponse<TokenResult>> Generate(string userName, string password)
        {
            SetBasicAuthorizationHeader();

            FormUrlEncodedContent requestContent = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("scope", $"{ApiScopeKey} offline_access"),
                }
            );

            HttpResponseMessage result = await PostAsync(
                CommerceAPIConstants.TokenUrl,
                requestContent
            );
            if (!result.IsSuccessStatusCode)
            {
                ErrorResponse error = await Task.Run(
                    () => ServiceBase.DeserializeModel<ErrorResponse>(result)
                );
                return new ServiceResponse<TokenResult> { Error = error, StatusCode = result.StatusCode };
            }

            TokenResult token = await Task.Run(
                () => ServiceBase.DeserializeModel<TokenResult>(result)
            );
            StoreAccessToken(token);
            return new ServiceResponse<TokenResult> { Model = token, StatusCode = result.StatusCode };
        }
        #endregion
    }
}
