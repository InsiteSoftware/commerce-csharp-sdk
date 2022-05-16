using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IAdminAuthenticationService : IAuthenticationService
    {
        Task<bool> ResetPassword(string userName);
    }
}
