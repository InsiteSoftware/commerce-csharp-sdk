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

        Task<ServiceResponse<AccountResult>> GetAccountsAsync();

        Task<ServiceResponse<Account>> GetCurrentAccountAsync();

        Task<ServiceResponse<Account>> PatchAccountAsync(Account account);

        Task<ServiceResponse<Account>> GetAccountIdAsync(Guid accountId);

        Task<ServiceResponse<Account>> PostAccountsAsync(Account account);

        Task<ServiceResponse<Account>> PatchAccountIdAsync(Guid accountId);

        Task<ServiceResponse<Account>> PatchShipToAddressAsync(Guid accountId);

        Task<ServiceResponse<ShipTo>> GetShipToAddressAsync(Guid accountId);

        Task<ServiceResponse<Account>> GetCurrentAccountPaymentProfileAsync();

        Task<ServiceResponse<Account>> PostCurrentAccountPaymentProfileAsync(Account account);

        Task<ServiceResponse<Account>> PatchCurrentAccountPaymentProfileIdAsync(
            Guid accountPaymentProfileId,
            Account account
        );

        Task<ServiceResponse<Account>> GetCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<bool> DeleteCurrentAccountPaymentProfileIdAsync(Guid accountPaymentProfileId);

        Task<ServiceResponse<Account>> PatchAccountsVmiAsync(Guid vmiUserId, Account account);

        Task<ServiceResponse<Account>> PostAccountsVmiAsync(Account account);

        /// <summary>
        /// A service which manages the account payment profile.
        /// </summary>
        Task<ServiceResponse<AccountPaymentProfileCollectionResult>> GetPaymentProfiles(
            PaymentProfileQueryParameters parameters = null
        );

        Task<ServiceResponse<AccountPaymentProfile>> GetPaymentProfile(Guid accountPaymentProfileId);

        Task<ServiceResponse<AccountPaymentProfile>> SavePaymentProfile(
            AccountPaymentProfile accountPaymentProfile
        );

        Task<bool> DeletePaymentProfile(Guid accountPaymentProfileId);
    }
}
