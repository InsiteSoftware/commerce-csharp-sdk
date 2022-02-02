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

        public AccountService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

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

        public async Task<Account> PatchAccount(Account account)
        {
            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(account));
                Account result = await PatchAsyncNoCache<Account>($"{AccountUrl}/current", stringContent);
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
