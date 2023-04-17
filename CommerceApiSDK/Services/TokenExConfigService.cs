using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
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
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<TokenExDto>> GetTokenExConfig(TokenExConfigQueryParameters parameters = null)
        {
            try
            {
                string url = CommerceAPIConstants.TokenExConfigUrl;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var response = await GetAsyncNoCache<TokenExDto>(
                    url,
                    DefaultRequestTimeout
                );

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<TokenExDto>(exception: exception);
            }
        }
    }
}
