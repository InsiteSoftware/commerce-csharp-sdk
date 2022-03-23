using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services
{
    /// <summary>
    /// High level authentication api
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOptiAPIBaseServiceProvider _optiAPIBaseServiceProvider;
        private IDisposable refreshTokenNotificationSubscription;

        public AuthenticationService(
            IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
        {
            _optiAPIBaseServiceProvider = optiAPIBaseServiceProvider;
            refreshTokenNotificationSubscription = _optiAPIBaseServiceProvider.GetMessengerService().Subscribe<RefreshTokenExpiredOptiMessage>(RefreshTokenExpiredHandler);
 
        }

        /// <summary>
        /// Logs a user into the server, saves their session locally, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not sign in was successful</returns>
        public virtual async Task<(bool, ErrorResponse)> LogInAsync(string userName, string password)
        {
            ServiceResponse<TokenResult> result = await _optiAPIBaseServiceProvider.GetClientService().Generate(userName, password);
            TokenResult tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            _optiAPIBaseServiceProvider.GetClientService().SetBearerAuthorizationHeader(tokenResult.AccessToken);
            _optiAPIBaseServiceProvider.GetClientService().StoreSessionState();

            Session session = new Session() { UserName = userName, Password = password };
            ServiceResponse<Session> sessionCreateResult = await _optiAPIBaseServiceProvider.GetSessionService().PostSession(session);
            Session createdSession = sessionCreateResult?.Model;
            if (createdSession == null)
            {
                _optiAPIBaseServiceProvider.GetClientService().SetBasicAuthorizationHeader();
                return (false, sessionCreateResult?.Error ?? ErrorResponse.Empty());
            }

            Session sessionPatchResult = await _optiAPIBaseServiceProvider.GetSessionService().PatchSession(createdSession);

            if (sessionPatchResult == null)
            {
                _optiAPIBaseServiceProvider.GetClientService().SetBasicAuthorizationHeader();
                return (false, ErrorResponse.Empty());
            }

            if (refreshTokenNotificationSubscription == null)
            {
                refreshTokenNotificationSubscription = _optiAPIBaseServiceProvider.GetMessengerService().Subscribe<RefreshTokenExpiredOptiMessage>(RefreshTokenExpiredHandler);
            }

            return (true, null);
        }

        /// <summary>
        /// Runs LogoutAsync within a Task context
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        public virtual void Logout(bool isRefreshTokenExpired = false)
        {
            Task logoutTask = Task.Run(async () => await LogoutAsync(isRefreshTokenExpired));
        }

        /// <summary>
        /// Logs the current user out of the server, then resets the currently stored session to a non-authenticated state
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        public virtual async Task LogoutAsync(bool isRefreshTokenExpired = false)
        {
            if (refreshTokenNotificationSubscription != null)
            {
                refreshTokenNotificationSubscription.Dispose();
                refreshTokenNotificationSubscription = null;
            }

            _optiAPIBaseServiceProvider.GetSessionService().ClearCache();

            Session clonedSession;
            if (_optiAPIBaseServiceProvider.GetSessionService().CurrentSession is Session currentSession)
            {
                clonedSession = currentSession.Clone() as Session;
            }
            else
            {
                clonedSession = await _optiAPIBaseServiceProvider.GetSessionService().GetCurrentSession();
            }

            if (clonedSession != null)
            {
                clonedSession.UserName = null;
                clonedSession.BillTo = null;
                clonedSession.ShipTo = null;
                clonedSession.Language = null;
                clonedSession.IsAuthenticated = false;
                await _optiAPIBaseServiceProvider.GetSessionService().PatchSession(clonedSession);
            }

            _optiAPIBaseServiceProvider.GetClientService().Reset();

            _optiAPIBaseServiceProvider.GetClientService().RemoveAccessToken();

            _optiAPIBaseServiceProvider.GetMessengerService().Publish(new UserSignedOutOptiMessage()
            {
                IsRefreshTokenExpired = isRefreshTokenExpired,
            });
        }

        /// <summary>
        /// Checks whether or not the application is currently logged in
        /// </summary>
        /// <returns>Boolean value for whether or not user is logged in</returns>
        public virtual async Task<bool> IsAuthenticatedAsync()
        {
            if (_optiAPIBaseServiceProvider.GetClientService().IsExistsAccessToken())
            {
                _ = await _optiAPIBaseServiceProvider.GetAccountService().GetCurrentAccountAsync();

                return _optiAPIBaseServiceProvider.GetClientService().IsExistsAccessToken();
            }

            return false;
        }

        protected virtual void RefreshTokenExpiredHandler(OptiMessage message)
        {
            LogoutAsync(true);
        }
    }
}
