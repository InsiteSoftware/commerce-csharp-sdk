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
        protected readonly IMessengerService OptiMessenger;

        private readonly IClientService clientService;
        private readonly IAccountService accountService;
        private readonly ICacheService cacheService;

        protected readonly ISessionService sessionService;

        private Guid? subscriptionId;

        public AuthenticationService(
            IClientService clientService,
            ISessionService sessionService,
            IMessengerService optiMessenger,
            IAccountService accountService,
            ICacheService cacheService
        )
        {
            this.clientService = clientService;
            this.sessionService = sessionService;
            this.OptiMessenger = optiMessenger;
            this.accountService = accountService;
            this.cacheService = cacheService;

            subscriptionId = this.OptiMessenger.Subscribe<RefreshTokenExpiredOptiMessage>(
                RefreshTokenExpiredHandler
            );
        }

        /// <summary>
        /// Logs a user into the server, saves their session locally, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not sign in was successful</returns>
        public virtual async Task<(bool, ErrorResponse)> LogInAsync(
            string userName,
            string password
        )
        {
            ServiceResponse<TokenResult> result = await this.clientService.Generate(
                userName,
                password
            );
            TokenResult tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            this.clientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            this.clientService.StoreSessionState();

            Session session = new Session() { UserName = userName, Password = password };
            ServiceResponse<Session> sessionCreateResult = await this.sessionService.PostSession(
                session
            );
            Session createdSession = sessionCreateResult?.Model;
            if (createdSession == null)
            {
                this.clientService.SetBasicAuthorizationHeader();
                return (false, sessionCreateResult?.Error ?? ErrorResponse.Empty());
            }

            Session sessionPatchResult = await this.sessionService.PatchSession(createdSession);

            if (sessionPatchResult == null)
            {
                this.clientService.SetBasicAuthorizationHeader();
                return (false, ErrorResponse.Empty());
            }

            if (subscriptionId == null)
            {
                subscriptionId = this.OptiMessenger.Subscribe<RefreshTokenExpiredOptiMessage>(
                    RefreshTokenExpiredHandler
                );
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
            if (subscriptionId.HasValue)
            {
                this.OptiMessenger.Unsubscribe<RefreshTokenExpiredOptiMessage>(
                    subscriptionId.Value
                );
                subscriptionId = null;
            }

            this.cacheService.ClearAllCaches();

            await this.sessionService.DeleteCurrentSession();

            this.clientService.Reset();

            this.clientService.RemoveAccessToken();

            this.OptiMessenger.Publish(
                new UserSignedOutOptiMessage() { IsRefreshTokenExpired = isRefreshTokenExpired, }
            );
        }

        /// <summary>
        /// Checks whether or not the application is currently logged in
        /// </summary>
        /// <returns>Boolean value for whether or not user is logged in</returns>
        public virtual async Task<bool> IsAuthenticatedAsync()
        {
            if (this.clientService.IsExistsAccessToken())
            {
                var currentSession = await this.sessionService.GetCurrentSession();
                if (currentSession != null && currentSession.IsAuthenticated)
                {
                    _ = await this.accountService.GetCurrentAccountAsync();

                    return true;
                }
                else
                {
                    this.clientService.RemoveAccessToken();
                }
            }

            return false;
        }

        protected virtual void RefreshTokenExpiredHandler(OptiMessage message)
        {
            LogoutAsync(true);
        }
    }
}
