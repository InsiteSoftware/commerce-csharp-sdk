using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class RealTimePricingService : ServiceBase, IRealTimePricingService
    {
        public RealTimePricingService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<GetRealTimePricingResult> GetProductRealTimePrices(
            RealTimePricingParameters parameters
        )
        {
            try
            {
                if (IsOnline)
                {
                    StringContent stringContent = await Task.Run(
                        () => SerializeModel(new { parameters })
                    );
                    GetRealTimePricingResult result =
                        await PostAsyncNoCache<GetRealTimePricingResult>(
                            CommerceAPIConstants.RealTimePricingUrl,
                            stringContent
                        );
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }
    }
}
