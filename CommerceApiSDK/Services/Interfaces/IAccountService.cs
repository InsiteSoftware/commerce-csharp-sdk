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

        Task<Account> PatchAccount(Account account);
    }
}
