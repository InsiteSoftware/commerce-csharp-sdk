﻿using System;
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
        public async Task<ServiceResponse<AccountResult>> GetAccountsAsync()
        {
            try
            {
                return await GetAsyncNoCache<AccountResult>(CommerceAPIConstants.AccountUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<AccountResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<Account>> GetCurrentAccountAsync()
        {
            try
            {
                var result = await GetAsyncNoCache<Account>(
                    $"{CommerceAPIConstants.AccountUrl}/current",
                    DefaultRequestTimeout
                );

                if (result.Model != null)
                {
                    CurrentAccount = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        public async Task<ServiceResponse<Account>> PatchAccountAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/current";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //POST:: /api/v1/accounts
        public async Task<ServiceResponse<Account>> PostAccountsAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //GET:: /api/v1/accounts/{accountId}
        public async Task<ServiceResponse<Account>> GetAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}";
                return await GetAsyncNoCache<Account>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}
        public async Task<ServiceResponse<Account>> PatchAccountIdAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                var result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //PATCH:: /api/v1/accounts/{accountId}/shiptos
        public async Task<ServiceResponse<Account>> PatchShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}/shiptos";
                StringContent stringContent = await Task.Run(() => SerializeModel(accountId));
                var result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //GET:: /api/v1/accounts/{AccountId}/shiptos
        public async Task<ServiceResponse<ShipTo>> GetShipToAddressAsync(Guid accountId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/{accountId}/shiptos";
                return await GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<ShipTo>(exception: exception);
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles
        public async Task<ServiceResponse<Account>> GetCurrentAccountPaymentProfileAsync()
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
                var result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result.Model != null)
                {
                    CurrentAccount = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //POST:: /api/v1/accounts/current/paymentprofiles
        public async Task<ServiceResponse<Account>> PostCurrentAccountPaymentProfileAsync(Account account)
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PostAsyncNoCache<Account>(url, stringContent);

                if (result.Model != null)
                {
                    CurrentAccount = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //PATCH:: /api/v1/accounts/current/paymentprofiles/{accountPaymentProfileId}
        public async Task<ServiceResponse<Account>> PatchCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId,
            Account account
        )
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PatchAsyncNoCache<Account>(url, null);

                if (result.Model != null)
                {
                    CurrentAccount = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //GET:: /api/v1/accounts/current/paymentprofiles/{AccountPaymentProfileId}
        public async Task<ServiceResponse<Account>> GetCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId
        )
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.AccountUrl}{CommerceAPIConstants.CurrentPaymentProfiles}{accountPaymentProfileId}";
                var result = await GetAsyncNoCache<Account>(url, DefaultRequestTimeout);

                if (result.Model != null)
                {
                    CurrentAccount = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
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
        public async Task<ServiceResponse<Account>> PatchAccountsVmiAsync(Guid vmiUserId, Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/vmi/{vmiUserId}";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PatchAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        //POST:: /api/v1/accounts/vmi/import
        public async Task<ServiceResponse<Account>> PostAccountsVmiAsync(Account account)
        {
            try
            {
                string url = $"{CommerceAPIConstants.AccountUrl}/vmi/import";
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                var result = await PostAsyncNoCache<Account>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Account>(exception: exception);
            }
        }

        public async Task<ServiceResponse<AccountPaymentProfileCollectionResult>> GetPaymentProfiles(
            PaymentProfileQueryParameters parameters = null
        )
        {
            try
            {
                string url =
                    parameters == null
                        ? CommerceAPIConstants.PaymentProfileUrl
                        : $"{CommerceAPIConstants.PaymentProfileUrl}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<AccountPaymentProfileCollectionResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<AccountPaymentProfileCollectionResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<AccountPaymentProfile>> GetPaymentProfile(Guid accountPaymentProfileId)
        {
            try
            {
                if (accountPaymentProfileId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(accountPaymentProfileId)} is empty");
                }

                string url = $"{CommerceAPIConstants.PaymentProfileUrl}/{accountPaymentProfileId}";
                var result = await GetAsyncNoCache<AccountPaymentProfile>(url);
                if (result.Model == null)
                {
                    throw new Exception("The account payment profile requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<AccountPaymentProfile>(exception: exception);
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
                        CommerceAPIConstants.PaymentProfileUrl,
                        stringContent
                    );
                }
                else
                {
                    string editUrl =
                        $"{CommerceAPIConstants.PaymentProfileUrl}/{accountPaymentProfile.Id}";
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
                return GetServiceResponse<AccountPaymentProfile>(exception: exception);
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

                string url = $"{CommerceAPIConstants.PaymentProfileUrl}/{accountPaymentProfileId}";
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
