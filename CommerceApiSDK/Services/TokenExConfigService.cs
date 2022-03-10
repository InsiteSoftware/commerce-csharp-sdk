using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class TokenExConfigService : ServiceBase, ITokenExConfigService
    {
        public TokenExConfigService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
             : base(clientService, networkService, trackingService, cacheService, loggerService)
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
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
