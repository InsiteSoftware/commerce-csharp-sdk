using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        private const string AccountUrl = "/api/v1/accounts";
        private const string CurrentPaymentProfiles = "/current/paymentprofiles";

        private Account currentAccount;

        public Account CurrentAccount
        {
            get => currentAccount;
            private set
            {
                currentAccount = value;

                if (!string.IsNullOrEmpty(currentAccount?.Id))
                {
                    TrackingService.SetUserID(currentAccount.Id);
                }
            }
        }

        public AccountService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        //GET:: /api/v1/accounts
        public async Task<AccountResult> GetAccountsAsync()
        {
            try
            {
                return await GetAsyncNoCache<AccountResult>(AccountUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Account> GetCurrentAccountAsync()
        {
            try
            {
                Account account = await GetAsyncNoCache<Account>($"{AccountUrl}/current", DefaultRequestTimeout);

                if (account != null)
                {
                    CurrentAccount = account;
                }

                return account;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Account> PatchAccountAsync(Account account)
        {
            try
            {
                string url = $"{AccountUrl}/current";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts
        public async Task<Account> PostAccountsAsync(Account account)
        {
            try
            {
                string url = $"{AccountUrl}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/{accountId}
        public async Task<Account> GetAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{AccountUrl}/{accountId}";
                return await GetAsyncNoCache<Account>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}
        public async Task<Account> PatchAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{AccountUrl}/{accountId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}/shiptos
        public async Task<Account> PatchShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{AccountUrl}/{accountId}/shiptos";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/{AccountId}/shiptos
        public async Task<ShipTo> GetShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{AccountUrl}/{accountId}/shiptos";
                return await GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> GetCurrentAccountPaymentProfileAsync()
        {
            try
            {
                string url = $"{AccountUrl}{CurrentPaymentProfiles}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> PostCurrentAccountPaymentProfileAsync(Account account)
        {
            try
            {
                string url = $"{AccountUrl}{CurrentPaymentProfiles}";
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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/current/paymentprofiles/{accountPaymentProfileId}
        public async Task<Account> PatchCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId, Account account)
        {
            try
            {
                string url = $"{AccountUrl}{CurrentPaymentProfiles}{accountPaymentProfileId}";
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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<Account> GetCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId)
        {
            try
            {
                string url = $"{AccountUrl}{CurrentPaymentProfiles}{accountPaymentProfileId}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //DELETE:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId)
        {
            try
            {
                string url = $"{AccountUrl}{CurrentPaymentProfiles}{accountPaymentProfileId}";
                HttpResponseMessage result = await DeleteAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }

        //PATCH:: /api/v1/accounts/vmi/{vmiUserId}
        public async Task<Account> PatchAccountsVmiAsync(Guid vmiUserId, Account account)
        {
            try
            {
                string url = $"{AccountUrl}/vmi/{vmiUserId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts/vmi/import
        public async Task<Account> PostAccountsVmiAsync(Account account)
        {
            try
            {
                string url = $"{AccountUrl}/vmi/import";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}