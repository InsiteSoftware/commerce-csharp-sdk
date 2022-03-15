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
        private readonly IClientService clientService;
        private readonly ISessionService sessionService;
        private readonly IMessengerService optiMessenger;
        private IDisposable refreshTokenNotificationSubscription;
        private readonly IAccountService accountService;

        public AuthenticationService(
            IClientService clientService,
            ISessionService sessionService,
            IMessengerService optiMessenger,
            IAccountService accountService)
        {
            this.clientService = clientService;
            this.sessionService = sessionService;
            this.optiMessenger = optiMessenger;
            refreshTokenNotificationSubscription = this.optiMessenger.Subscribe<RefreshTokenExpiredOptiMessage>(RefreshTokenExpiredHandler);
            this.accountService = accountService;
        }

        /// <summary>
        /// Logs a user into the server, saves their session locally, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not sign in was successful</returns>
        public virtual async Task<(bool, ErrorResponse)> LogInAsync(string userName, string password)
        {
            ServiceResponse<TokenResult> result = await clientService.Generate(userName, password);
            TokenResult tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            clientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            clientService.StoreSessionState();

            Session session = new Session() { UserName = userName, Password = password };
            ServiceResponse<Session> sessionCreateResult = await sessionService.PostSession(session);
            Session createdSession = sessionCreateResult?.Model;
            if (createdSession == null)
            {
                clientService.SetBasicAuthorizationHeader();
                return (false, sessionCreateResult?.Error ?? ErrorResponse.Empty());
            }

            Session sessionPatchResult = await sessionService.PatchSession(createdSession);

            if (sessionPatchResult == null)
            {
                clientService.SetBasicAuthorizationHeader();
                return (false, ErrorResponse.Empty());
            }

            if (refreshTokenNotificationSubscription == null)
            {
                refreshTokenNotificationSubscription = optiMessenger.Subscribe<RefreshTokenExpiredOptiMessage>(RefreshTokenExpiredHandler);
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

            sessionService.ClearCache();

            Session clonedSession;
            if (sessionService.CurrentSession is Session currentSession)
            {
                clonedSession = currentSession.Clone() as Session;
            }
            else
            {
                clonedSession = await sessionService.GetCurrentSession();
            }

            if (clonedSession != null)
            {
                clonedSession.UserName = null;
                clonedSession.BillTo = null;
                clonedSession.ShipTo = null;
                clonedSession.Language = null;
                clonedSession.IsAuthenticated = false;
                await sessionService.PatchSession(clonedSession);
            }

            clientService.Reset();

            clientService.RemoveAccessToken();

            optiMessenger.Publish(new UserSignedOutOptiMessage()
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
            if (clientService.IsExistsAccessToken())
            {
                _ = await accountService.GetCurrentAccountAsync();

                return clientService.IsExistsAccessToken();
            }

            return false;
        }

        protected virtual void RefreshTokenExpiredHandler(OptiMessage message)
        {
            LogoutAsync(true);
        }
    }
}
