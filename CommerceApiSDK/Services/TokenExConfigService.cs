using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class TokenExConfigService : ServiceBase, ITokenExConfigService
    {
        public TokenExConfigService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService)
             : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService)
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
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
