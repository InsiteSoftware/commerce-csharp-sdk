using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        private Account currentAccount;

        public Account CurrentAccount
        {
            get => currentAccount;
            private set
            {
                currentAccount = value;

                if (!string.IsNullOrEmpty(currentAccount?.Id))
                {
                    this.TrackingService.SetUserID(currentAccount.Id);
                }
            }
        }

        public AccountService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService
        ) : base(clientService, networkService, trackingService, cacheService, loggerService) { }

        //GET:: /api/v1/accounts
        public async Task<AccountResult> GetAccountsAsync()
        {
            try
            {
                return await GetAsyncNoCache<AccountResult>(CommerceAPIConstants.AccountUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Account> GetCurrentAccountAsync()
        {
            try
            {
                Account account = await GetAsyncNoCache<Account>(
                    $"{CommerceAPIConstants.AccountUrl}/current",
                    DefaultRequestTimeout
                );

                if (account != null)
                {
                    CurrentAccount = account;
                }

                return account;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Account> PatchAccountAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/current";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts
        public async Task<Account> PostAccountsAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/{accountId}
        public async Task<Account> GetAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}";
                return await GetAsyncNoCache<Account>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}
        public async Task<Account> PatchAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}/shiptos
        public async Task<Account> PatchShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}/shiptos";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/{AccountId}/shiptos
        public async Task<ShipTo> GetShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}/shiptos";
                return await GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> GetCurrentAccountPaymentProfileAsync()
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> PostCurrentAccountPaymentProfileAsync(Account account)
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PostAsyncNoCache<Account>(url, stringContent);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/current/paymentprofiles/{accountPaymentProfileId}
        public async Task<Account> PatchCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId,
            Account account
        )
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>(url, null);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<Account> GetCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId
        )
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //DELETE:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId
        )
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                HttpResponseMessage result = await DeleteAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        //PATCH:: /api/v1/accounts/vmi/{vmiUserId}
        public async Task<Account> PatchAccountsVmiAsync(Guid vmiUserId, Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/vmi/{vmiUserId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts/vmi/import
        public async Task<Account> PostAccountsVmiAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/vmi/import";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AccountPaymentProfileCollectionResult> GetPaymentProfiles(
            PaymentProfileQueryParameters parameters = null
        )
        {
            try
            {
                string url =
                    parameters == null
                        ? CommerceAPIConstants.PaymentProfileUri
                        : $"{CommerceAPIConstants.PaymentProfileUri}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<AccountPaymentProfileCollectionResult>(url);
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

                string url = $"{CommerceAPIConstants.PaymentProfileUri}/{accountPaymentProfileId}";
                AccountPaymentProfile result = await GetAsyncNoCache<AccountPaymentProfile>(url);
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

        public async Task<ServiceResponse<AccountPaymentProfile>> SavePaymentProfile(
            AccountPaymentProfile accountPaymentProfile
        )
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
                StringContent stringContent = await Task.Run(
                    () => SerializeModel(accountPaymentProfile)
                );
                if (accountPaymentProfile.Id.Equals(Guid.Empty.ToString()))
                {
                    response = await PostAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(
                        CommerceAPIConstants.PaymentProfileUri,
                        stringContent
                    );
                }
                else
                {
                    string editUrl =
                        $"{CommerceAPIConstants.PaymentProfileUri}/{accountPaymentProfile.Id}";
                    response = await PatchAsyncNoCacheWithErrorMessage<AccountPaymentProfile>(
                        editUrl,
                        stringContent
                    );
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

                string url = $"{CommerceAPIConstants.PaymentProfileUri}/{accountPaymentProfileId}";
                HttpResponseMessage deleteResponse = await DeleteAsync(url);
                return deleteResponse != null && deleteResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }
    }
}
