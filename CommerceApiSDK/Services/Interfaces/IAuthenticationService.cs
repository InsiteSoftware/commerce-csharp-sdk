using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// High level authentication api
    /// </summary>
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> LogInAsync(string userName, string password);

        void Logout(bool isRefreshTokenExpired = false);

        Task LogoutAsync(bool isRefreshTokenExpired = false);

        Task<ServiceResponse<bool>> IsAuthenticatedAsync();
    }
}
