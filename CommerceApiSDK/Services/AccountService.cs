namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class AccountService : ServiceBase, IAccountService
    {
        private const string AccountUrl = "/api/v1/accounts";

        private Account currentAccount;

        public Account CurrentAccount
        {
            get => this.currentAccount;
            private set
            {
                this.currentAccount = value;

                if (!string.IsNullOrEmpty(this.currentAccount?.Id))
                {
                    this.TrackingService.SetUserID(this.currentAccount.Id);
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
                return await this.GetAsyncNoCache<AccountResult>(AccountUrl);
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
                var account = await this.GetAsyncNoCache<Account>($"{AccountUrl}/current", ServiceBase.DefaultRequestTimeout);

                if (account != null)
                {
                    this.CurrentAccount = account;
                }

                return account;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Account> PatchAccount(Account account)
        {
            try
            {
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(account));
                var result = await this.PatchAsyncNoCache<Account>($"{AccountUrl}/current", stringContent);
                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
