namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IAdminAuthenticationService : IAuthenticationService
    {
        Task<bool> ResetPassword(string userName);
    }
}