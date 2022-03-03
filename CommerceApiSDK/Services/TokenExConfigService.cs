using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class TokenExConfigService : ServiceBase, ITokenExConfigService
    {
        private const string TokenexconfigUrl = "/api/v1/tokenexconfig";

        public TokenExConfigService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
             : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<TokenExDto> GetTokenexconfigAsync()
        {
            try
            {
                TokenExDto tokenexConfig= await GetAsyncNoCache<TokenExDto>(TokenexconfigUrl, DefaultRequestTimeout);

                if (tokenexConfig == null)
                {
                    return null;
                }

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
