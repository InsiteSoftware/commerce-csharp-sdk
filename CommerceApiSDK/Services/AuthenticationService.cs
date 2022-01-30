namespace CommerceApiSDK.Services
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Services.Messages;
    using MvvmCross.Plugin.Messenger;

    /// <summary>
    /// High level authentication api
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClientService clientService;
        private readonly ISessionService sessionService;
        private readonly IMvxMessenger messenger;
        private MvxSubscriptionToken refreshTokenNotificationSubscription;
        private readonly IAccountService accountService;

        public AuthenticationService(
            IClientService clientService,
            ISessionService sessionService,
            IMvxMessenger messenger,
            IAccountService accountService)
        {
            this.clientService = clientService;
            this.sessionService = sessionService;
            this.messenger = messenger;
            this.refreshTokenNotificationSubscription = this.messenger.Subscribe<RefreshTokenExpiredMessage>(this.RefreshTokenExpiredHandler);
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
            var result = await this.clientService.Generate(userName, password);
            var tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            this.clientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            this.clientService.StoreSessionState();

            var session = new Session { UserName = userName, Password = password };
            var sessionCreateResult = await this.sessionService.PostSession(session);
            var createdSession = sessionCreateResult?.Model;
            if (createdSession == null)
            {
                this.clientService.SetBasicAuthorizationHeader();
                return (false, sessionCreateResult?.Error ?? ErrorResponse.Empty());
            }

            var sessionPatchResult = await this.sessionService.PatchSession(createdSession);

            if (sessionPatchResult == null)
            {
                this.clientService.SetBasicAuthorizationHeader();
                return (false, ErrorResponse.Empty());
            }

            if (this.refreshTokenNotificationSubscription == null)
            {
                this.refreshTokenNotificationSubscription = this.messenger.Subscribe<RefreshTokenExpiredMessage>(this.RefreshTokenExpiredHandler);
            }

            return (true, null);
        }

        /// <summary>
        /// Runs LogoutAsync within a Task context
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        public virtual void Logout(bool isRefreshTokenExpired = false)
        {
            var logoutTask = Task.Run(async () => await this.LogoutAsync(isRefreshTokenExpired));
        }

        /// <summary>
        /// Logs the current user out of the server, then resets the currently stored session to a non-authenticated state
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        public virtual async Task LogoutAsync(bool isRefreshTokenExpired = false)
        {
            if (this.refreshTokenNotificationSubscription != null)
            {
                this.refreshTokenNotificationSubscription.Dispose();
                this.refreshTokenNotificationSubscription = null;
            }

            this.sessionService.ClearCache();

            Session clonedSession;
            if (this.sessionService.CurrentSession is Session currentSession)
            {
                clonedSession = currentSession.Clone() as Session;
            }
            else
            {
                clonedSession = await this.sessionService.GetCurrentSession();
            }

            if (clonedSession != null)
            {
                clonedSession.UserName = null;
                clonedSession.BillTo = null;
                clonedSession.ShipTo = null;
                clonedSession.Language = null;
                clonedSession.IsAuthenticated = false;
                await this.sessionService.PatchSession(clonedSession);
            }

            this.clientService.Reset();

            this.clientService.RemoveAccessToken();

            this.messenger.Publish(new UserSignedOutMessage(this)
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
            if (this.clientService.IsExistsAccessToken())
            {
                _ = await this.accountService.GetCurrentAccountAsync();

                return this.clientService.IsExistsAccessToken();
            }

            return false;
        }

        protected virtual void RefreshTokenExpiredHandler(MvxMessage message)
        {
            this.LogoutAsync(true);
        }
    }
}
