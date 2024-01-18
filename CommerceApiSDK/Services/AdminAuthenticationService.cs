using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
    public class AdminAuthenticationService : AuthenticationService, IAdminAuthenticationService
    {
        private readonly IAdminClientService adminClientService;

        private OptiSubscriptionToken subscriptionToken;

        public AdminAuthenticationService(
            IClientService clientService,
            ISessionService sessionService,
            IMessengerService OptiMessenger,
            IAccountService accountService,
            ICacheService cacheService,
            IAdminClientService adminClientService
        )
            : base(clientService, sessionService, OptiMessenger, accountService, cacheService)
        {
            this.adminClientService = adminClientService;

            subscriptionToken = this.OptiMessenger.Subscribe<AdminRefreshTokenExpiredOptiMessage>(
                AdminRefreshTokenExpiredHandler
            );
        }

        /// <summary>
        /// Logs a user into the server, then returns whether or not it was successful
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>Whether or not Login was successful</returns>
        public override async Task<ServiceResponse<bool>> LogInAsync(
            string userName,
            string password
        )
        {
            ServiceResponse<TokenResult> result = await this.adminClientService.Generate(
                $"admin_{userName}",
                password
            );
            TokenResult tokenResult = result?.Model;
            if (tokenResult == null)
            {
                return new ServiceResponse<bool>
                {
                    Model = false,
                    Error = result?.Error ?? ErrorResponse.Empty(),
                    StatusCode = result.StatusCode
                };
            }

            if (subscriptionToken == null)
            {
                subscriptionToken =
                    this.OptiMessenger.Subscribe<AdminRefreshTokenExpiredOptiMessage>(
                        AdminRefreshTokenExpiredHandler
                    );
            }

            this.adminClientService.SetBearerAuthorizationHeader(tokenResult.AccessToken);
            this.adminClientService.StoreSessionState();

            return new ServiceResponse<bool> { Model = true, StatusCode = result.StatusCode };
        }

        /// <summary>
        /// Logs the current user out of the server, then resets the currently stored session to a non-authenticated state
        /// </summary>
        /// <param name="isRefreshTokenExpired">Whether or not logout was due to refresh token being expired</param>
        /// <returns></returns>
        public override async Task LogoutAsync(bool isRefreshTokenExpired = false)
        {
            if (subscriptionToken != null)
            {
                this.OptiMessenger.Unsubscribe<AdminRefreshTokenExpiredOptiMessage>(
                    subscriptionToken.Id
                );
                subscriptionToken.Dispose();
                subscriptionToken = null;
            }

            _ = await this.adminClientService.GetAsync(
                "identity/connect/endsession",
                ServiceBase.DefaultRequestTimeout
            );
            this.adminClientService.Reset();

            this.OptiMessenger.Publish(
                new AdminSignedOutOptiMessage() { IsRefreshTokenExpired = isRefreshTokenExpired, }
            );
            this.adminClientService.RemoveAccessToken();
        }

        /// <summary>
        /// Checks whether or not the application is currently logged in
        /// </summary>
        /// <returns>Boolean value for whether or not user is logged in</returns>
        public override async Task<ServiceResponse<bool>> IsAuthenticatedAsync()
        {
            if (this.adminClientService.IsExistsAccessToken())
            {
                await this.adminClientService.GetAsync(
                    CommerceAPIConstants.AdminUserProfileUrl,
                    ServiceBase.DefaultRequestTimeout
                );

                return new ServiceResponse<bool>
                {
                    Model = this.adminClientService.IsExistsAccessToken()
                };
            }

            return new ServiceResponse<bool> { Model = false };
        }

        /// <summary>
        /// Sends a request to the server to start the reset password flow for the given userName
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <returns>Whether or not request was a success</returns>
        public async Task<ServiceResponse<bool>> ResetPassword(string userName)
        {
            JsonSerializerSettings serializationSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            StringContent stringContent = await Task.Run(
                () => ServiceBase.SerializeModel(payload, serializationSettings)
            );

            var response = await this.adminClientService.PostAsync(
                CommerceAPIConstants.ResetPasswordUrl,
                stringContent,
                ServiceBase.DefaultRequestTimeout
            );

            HttpResponseMessage httpResponseMessage = response;

            if (
                httpResponseMessage.StatusCode == HttpStatusCode.Created
                || httpResponseMessage.StatusCode == HttpStatusCode.OK
            )
            {
                return new ServiceResponse<bool>
                {
                    Model = true,
                    StatusCode = httpResponseMessage.StatusCode
                };
            }
            else
            {
                return new ServiceResponse<bool>
                {
                    Model = false,
                    StatusCode = httpResponseMessage.StatusCode
                };
            }
        }

        private void AdminRefreshTokenExpiredHandler(OptiMessage message)
        {
            Logout(true);
        }

        protected override void RefreshTokenExpiredHandler(OptiMessage message)
        {
            // this method should do nothing because the AuthenticationService is handling this expiration of the Client Service
        }
    }
}
