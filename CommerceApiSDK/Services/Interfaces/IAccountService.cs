using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which fetches the user account.
    /// </summary>
    public interface IAccountService
    {
        Account CurrentAccount { get; }

        Task<AccountResult> GetAccountsAsync();

        Task<Account> GetCurrentAccountAsync();

        Task<Account> PatchAccountAsync(Account account);

        Task<Account> GetAccountIdAsync(Guid accountId);

        Task<Account> PostAccountsAsync(Account account);

        Task<Account> PatchAccountIdAsync(Guid accountId);

        Task<Account> PatchShipToAddressAsync(Guid accountId);

        Task<ShipTo> GetShipToAddressAsync(Guid accountId);

        Task<Account> GetCurrentAccountPaymentProfileAsync();

        Task<Account> PostCurrentAccountPaymentProfileAsync(Account account);

        Task<Account> PatchCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId, Account account);

        Task<Account> GetCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<Account> PatchAccountsVmiAsync(Guid vmiUserId, Account account);

        Task<Account> PostAccountsVmiAsync(Account account);
    }
}
