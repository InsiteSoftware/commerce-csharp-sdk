using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A REST client service.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Host name to connect to.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Protocol and Host to connect to.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Whether this should be a secure http connection.
        /// </summary>
        bool IsSecure { get; set; }

        /// <summary>
        /// Gets a representation of the client session state. This might be used as a key
        /// </summary>
        string SessionStateKey { get; }

        /// <summary>
        /// Debugging error message
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Http get.
        /// </summary>
        /// <param name="path">The url path.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <returns>Response message</returns>
        Task<HttpResponseMessage> GetAsync(
            string path,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Http get for not Insite domain.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <returns>Response message</returns>
        Task<HttpResponseMessage> GetAsyncNoHost(
            string url,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Http post.
        /// </summary>
        /// <param name="path">The url path.</param>
        /// <param name="content">The body content.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Http delete.
        /// </summary>
        /// <param name="path">The url path.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(
            string path,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Http patch.
        /// </summary>
        /// <param name="path">The url path.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <param name="content">The body content.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PatchAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Http put.
        /// </summary>
        /// <param name="path">The url path.</param>
        /// <param name="content">The body content.</param>
        /// <param name="timeout">Optional, request specific timeout interval.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(
            string path,
            HttpContent content,
            TimeSpan? timeout = null,
            CancellationToken? cancellationToken = null
        );

        /// <summary>
        /// Save client session state in app storage.
        /// </summary>
        void StoreSessionState(Session session = null);

        /// <summary>
        /// Load client session state from storage.
        /// </summary>
        void LoadSessionState();

        /// <summary>
        /// Set or clear cookie .
        /// </summary>
        void SetCookie(Cookie cookie);

        /// <summary>
        /// Set the Authorization header to use the basic scheme using the instance's
        /// clientId and clientSecret
        /// </summary>
        void SetBasicAuthorizationHeader();

        /// <summary>
        /// Set the Authorization header to use the bearer theme
        /// </summary>
        /// <param name="token">The access token</param>
        void SetBearerAuthorizationHeader(string token);

        /// <summary>
        /// Resets the HttpClient to original state
        /// </summary>
        void Reset();

        /// <summary>
        /// RenewAuthenticationTokens
        /// </summary>
        Task<bool> RenewAuthenticationTokens();

        /// Create the absolute path from the relative path
        /// </summary>
        /// <param name="relativePath">The relative path</param>
        /// <returns>The absolute path</returns>
        string MakeUrl(string relativePath);

        Task<ServiceResponse<TokenResult>> Generate(string userName, string password);

        Task<string> GetAccessToken();

        /// <summary>
        /// Removes AlternateCart cookie from CookieCollection that was set by CreateAlternateCart from ICartService
        /// </summary>
        Task RemoveAlternateCartCookie();

        void StoreAccessToken(TokenResult tokens);

        void RemoveAccessToken();

        bool IsExistsAccessToken();
    }
}
