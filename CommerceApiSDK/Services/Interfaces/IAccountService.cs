using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
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

        Task<ServiceResponse<Account>> PatchAccountAsync(Account account);

        Task<Account> GetAccountIdAsync(Guid accountId);

        Task<ServiceResponse<Account>> PostAccountsAsync(Account account);

        Task<ServiceResponse<Account>> PatchAccountIdAsync(Guid accountId);

        Task<ServiceResponse<Account>> PatchShipToAddressAsync(Guid accountId);

        Task<ShipTo> GetShipToAddressAsync(Guid accountId);

        Task<Account> GetCurrentAccountPaymentProfileAsync();

        Task<ServiceResponse<Account>> PostCurrentAccountPaymentProfileAsync(Account account);

        Task<ServiceResponse<Account>> PatchCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId,
            Account account
        );

        Task<Account> GetCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<ServiceResponse<Account>> PatchAccountsVmiAsync(Guid vmiUserId, Account account);

        Task<ServiceResponse<Account>> PostAccountsVmiAsync(Account account);

        /// <summary>
        /// A service which manages the account payment profile.
        /// </summary>
        Task<AccountPaymentProfileCollectionResult> GetPaymentProfiles(
            PaymentProfileQueryParameters parameters = null
        );

        Task<AccountPaymentProfile> GetPaymentProfile(Guid accountPaymentProfileId);

        Task<ServiceResponse<AccountPaymentProfile>> SavePaymentProfile(
            AccountPaymentProfile accountPaymentProfile
        );

        Task<bool> DeletePaymentProfile(Guid accountPaymentProfileId);
    }
}
