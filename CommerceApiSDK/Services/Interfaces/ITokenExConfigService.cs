using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ITokenExConfigService
    {
        Task<ServiceResponse<TokenExDto>> GetTokenExConfig(
            TokenExConfigQueryParameters parameters = null
        );
    }
}
