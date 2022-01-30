namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Services.Interfaces;

    public class PaymentProfileService : ServiceBase, IPaymentProfileService
    {
        private const string PaymentProfileUri = "/api/v1/accounts/current/paymentprofiles";
        private const string CartUri = "/api/v1/carts/current";

        public PaymentProfileService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<AccountPaymentProfileCollection> GetPaymentProfiles(PaymentProfileQueryParameters parameters = null)
        {
            try
            {
                var url = parameters == null ? PaymentProfileUri : $"{PaymentProfileUri}{parameters.ToQueryString()}";
                return await this.GetAsyncNoCache<AccountPaymentProfileCollection>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AccountPaymentProfile> GetPaymentProfile(Guid accountPaymentProfileId)
        {
            try
            {
                if (accountPaymentProfileId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(accountPaymentProfileId)} is empty");
                }

                var url = $"{PaymentProfileUri}/{accountPaymentProfileId}";
                var result = await this.GetAsyncNoCache<AccountPaymentProfile>(url);
                if (result == null)
                {
                    throw new Exception("The account payment profile requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<AccountPaymentProfile>> SavePaymentProfile(AccountPaymentProfile accountPaymentProfile)
        {
            try
            {
                if (accountPaymentProfile == null)
                {
                    throw new ArgumentException($"{nameof(accountPaymentProfile)} is null");
                }

                if (string.IsNullOrEmpty(accountPaymentProfile.Id))
                {
                    accountPaymentProfile.Id = Guid.Empty.ToString();
                }

                ServiceResponse<AccountPaymentProfile> response;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(accountPaymentProfile));
                if (accountPaymentProfile.Id.Equals(Guid.Empty.ToString()))
                {
                    response = await this.PostAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(PaymentProfileUri, stringContent);
                }
                else
                {
                    var editUrl = $"{PaymentProfileUri}/{accountPaymentProfile.Id}";
                    response = await this.PatchAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(editUrl, stringContent);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> DeletePaymentProfile(Guid accountPaymentProfileId)
        {
            try
            {
                if (accountPaymentProfileId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(accountPaymentProfileId)} is empty");
                }

                var url = $"{PaymentProfileUri}/{accountPaymentProfileId}";
                var deleteResponse = await this.DeleteAsync(url);
                return deleteResponse != null && deleteResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<Cart> GetPaymentCurrentCart(PaymentProfileQueryParameters parameters)
        {
            try
            {
                var url = parameters?.Expand != null ? $"{CartUri}{parameters.ToQueryString()}" : CartUri;
                return await this.GetAsyncNoCache<Cart>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
