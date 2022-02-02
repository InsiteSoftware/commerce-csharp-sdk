using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
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
                string url = parameters == null ? PaymentProfileUri : $"{PaymentProfileUri}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<AccountPaymentProfileCollection>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{PaymentProfileUri}/{accountPaymentProfileId}";
                AccountPaymentProfile result = await GetAsyncNoCache<AccountPaymentProfile>(url);
                if (result == null)
                {
                    throw new Exception("The account payment profile requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                StringContent stringContent = await Task.Run(() => SerializeModel(accountPaymentProfile));
                if (accountPaymentProfile.Id.Equals(Guid.Empty.ToString()))
                {
                    response = await PostAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(PaymentProfileUri, stringContent);
                }
                else
                {
                    string editUrl = $"{PaymentProfileUri}/{accountPaymentProfile.Id}";
                    response = await PatchAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(editUrl, stringContent);
                }

                return response;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{PaymentProfileUri}/{accountPaymentProfileId}";
                HttpResponseMessage deleteResponse = await DeleteAsync(url);
                return deleteResponse != null && deleteResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<Cart> GetPaymentCurrentCart(PaymentProfileQueryParameters parameters)
        {
            try
            {
                string url = parameters?.Expand != null ? $"{CartUri}{parameters.ToQueryString()}" : CartUri;
                return await GetAsyncNoCache<Cart>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
