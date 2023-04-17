using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IAdminAuthenticationService : IAuthenticationService
    {
        Task<ServiceResponse<bool>> ResetPassword(string userName);
    }
}
