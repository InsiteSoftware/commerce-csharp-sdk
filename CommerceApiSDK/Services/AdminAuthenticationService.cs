using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;
using MvvmCross.Plugin.Messenger;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
    public class AdminAuthenticationService : AuthenticationService, IAdminAuthenticationService
    {
        private readonly IAdminClientService adminClientService;

        private readonly IMvxMessenger messenger;
        private MvxSubscriptionToken adminRefreshTokenNotificationSubscription;

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
            adminRefreshTokenNotificationSubscription = this.messenger.Subscribe<AdminRefreshTokenExpiredMessage>(AdminRefreshTokenExpiredHandler);
        }

        /// <summary>
        /// Logs a user into the server, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not Login was successful</returns>
        public override async Task<(bool, ErrorResponse)> LogInAsync(string userName, string password)
        {
            ServiceResponse<TokenResult> result = await adminClientService.Generate($"admin_{userName}", password);
            TokenResult tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return (false, result?.Error ?? ErrorResponse.Empty());
            }

            adminClientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            adminClientService.StoreSessionState();

            if (adminRefreshTokenNotificationSubscription == null)
            {
                adminRefreshTokenNotificationSubscription = messenger.Subscribe<AdminRefreshTokenExpiredMessage>(AdminRefreshTokenExpiredHandler);
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
            if (adminRefreshTokenNotificationSubscription != null)
            {
                adminRefreshTokenNotificationSubscription.Dispose();
                adminRefreshTokenNotificationSubscription = null;
            }

            _ = await adminClientService.GetAsync("identity/connect/endsession", ServiceBase.DefaultRequestTimeout);
            adminClientService.Reset();

            adminClientService.RemoveAccessToken();

            messenger.Publish(new AdminSignedOutMessage(this)
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
            if (adminClientService.IsExistsAccessToken())
            {
                await adminClientService.GetAsync(CommerceAPIConstants.AdminUserProfileUri, ServiceBase.DefaultRequestTimeout);

                return adminClientService.IsExistsAccessToken();
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
            JsonSerializerSettings serializationSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            Dictionary<string, string> payload = new Dictionary<string, string> { { "userName", userName } };
            StringContent stringContent = await Task.Run(() => ServiceBase.SerializeModel(payload, serializationSettings));

            HttpResponseMessage httpResponseMessage = await adminClientService.PostAsync(CommerceAPIConstants.ResetPasswordUri, stringContent, ServiceBase.DefaultRequestTimeout);

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
            Logout(true);
        }

        protected override void RefreshTokenExpiredHandler(MvxMessage message)
        {
            // this method should do nothing because the AuthenticationService is handling this expiration of the Client Service
        }
    }
}