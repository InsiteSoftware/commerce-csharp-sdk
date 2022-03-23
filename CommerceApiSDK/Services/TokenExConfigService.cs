using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class TokenExConfigService : ServiceBase, ITokenExConfigService
    {
        public TokenExConfigService(IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
             : base(optiAPIBaseServiceProvider)
        {
        }

        public async Task<TokenExDto> GetTokenexconfigAsync()
        {
            try
            {
                TokenExDto tokenexConfig= await GetAsyncNoCache<TokenExDto>(CommerceAPIConstants.TokenexconfigUrl, DefaultRequestTimeout);
                
                return tokenexConfig;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}
