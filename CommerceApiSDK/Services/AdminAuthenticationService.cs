namespace CommerceApiSDK.Services
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Services.Messages;
    using MvvmCross.Plugin.Messenger;
    using Newtonsoft.Json;

    public class AdminAuthenticationService : AuthenticationService, IAdminAuthenticationService
    {
        private readonly IAdminClientService adminClientService;

        private readonly IMvxMessenger messenger;
        private MvxSubscriptionToken adminRefreshTokenNotificationSubscription;

        private const string ResetPasswordUri = "/admin/account/ForgotPassword";
        private const string AdminUserProfileUri = "/api/v1/admin/AdminUserProfiles/Default.Default()";

        public AdminAuthenticationService(
            IClientService clientService,
            ISessionService sessionService,
            IMvxMessenger messenger,
            IAccountService accountService,
            IAdminClientService adminClientService)
            : base(
                clientService,
                sessionService,
                messenger,
                accountService)
        {
            this.adminClientService = adminClientService;
            this.messenger = messenger;
            this.adminRefreshTokenNotificationSubscription = this.messenger.Subscribe<AdminRefreshTokenExpiredMessage>(this.AdminRefreshTokenExpiredHandler);
        }

        /// <summary>
        /// Logs a user into the server, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not Login was successful</returns>
        public override async Task<(bool, ErrorResponse)> LogInAsync(string userName, string password)
        {
            var result = await this.adminClientService.Generate($"admin_{userName}", password);
            var tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            this.adminClientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            this.adminClientService.StoreSessionState();

            if (this.adminRefreshTokenNotificationSubscription == null)
            {
                this.adminRefreshTokenNotificationSubscription = this.messenger.Subscribe<AdminRefreshTokenExpiredMessage>(this.AdminRefreshTokenExpiredHandler);
            }

            return (true, null);
        }

        /// <summary>
        /// Logs the current user out of the server, then resets the currently stored session to a non-authenticated state
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        /// <returns></returns>
        public override async Task LogoutAsync(bool isRefreshTokenExpired = false)
        {
            if (this.adminRefreshTokenNotificationSubscription != null)
            {
                this.adminRefreshTokenNotificationSubscription.Dispose();
                this.adminRefreshTokenNotificationSubscription = null;
            }

            _ = await this.adminClientService.GetAsync("identity/connect/endsession", ServiceBase.DefaultRequestTimeout);
            this.adminClientService.Reset();

            this.adminClientService.RemoveAccessToken();

            this.messenger.Publish(new AdminSignedOutMessage(this)
            {
                IsRefreshTokenExpired = isRefreshTokenExpired,
            });
        }

        /// <summary>
        /// Checks whether or not the application is currently logged in
        /// </summary>
        /// <returns>Boolean value for whether or not user is logged in</returns>
        public override async Task<bool> IsAuthenticatedAsync()
        {
            if (this.adminClientService.IsExistsAccessToken())
            {
                await this.adminClientService.GetAsync(AdminUserProfileUri, ServiceBase.DefaultRequestTimeout);

                return this.adminClientService.IsExistsAccessToken();
            }

            return false;
        }

        /// <summary>
        /// Sends a request to the server to start the reset password flow for the given userName
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <returns>Whether or not request was a success</returns>
        public async Task<bool> ResetPassword(string userName)
        {
            var serializationSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var payload = new Dictionary<string, string> { { "userName", userName } };
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(payload, serializationSettings));

            var httpResponseMessage = await this.adminClientService.PostAsync(ResetPasswordUri, stringContent, ServiceBase.DefaultRequestTimeout);

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created || httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AdminRefreshTokenExpiredHandler(MvxMessage message)
        {
            this.Logout(true);
        }

        protected override void RefreshTokenExpiredHandler(MvxMessage message)
        {
            // this method should do nothing because the AuthenticationService is handling this expiration of the Client Service
        }
    }
}