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
        private Account currentAccount;

        public Account CurrentAccount
        {
            get => currentAccount;
            private set
            {
                currentAccount = value;

                if (!string.IsNullOrEmpty(currentAccount?.Id))
                {
                    _commerceAPIServiceProvider.GetTrackingService().SetUserID(currentAccount.Id);
                }
            }
        }

        public AccountService(ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
        {
        }

        //GET:: /api/v1/accounts
        public async Task<AccountResult> GetAccountsAsync()
        {
            try
            {
                return await GetAsyncNoCache<AccountResult>(CommerceAPIConstants.AccountUrl);
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<Account> GetCurrentAccountAsync()
        {
            try
            {
                Account account = await GetAsyncNoCache<Account>($"{CommerceAPIConstants.AccountUrl}/current", DefaultRequestTimeout);

                if (account != null)
                {
                    CurrentAccount = account;
                }

                return account;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> GetCurrentAccountPaymentProfileAsync()
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        //POST:: /api/v1/accounts/current/paymentprofiles
        public async Task<Account> PostCurrentAccountPaymentProfileAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        //PATCH:: /api/v1/accounts/current/paymentprofiles/{accountPaymentProfileId}
        public async Task<Account> PatchCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId, Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<Account> GetCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                Account result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result != null)
                {
                    CurrentAccount = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        //DELETE:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                HttpResponseMessage result = await DeleteAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}