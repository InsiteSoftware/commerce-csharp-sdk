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

        public async Task<ServiceResponse<GetRealTimePricingResult>> GetProductRealTimePrices(
            RealTimePricingParameters parameters
        )
        {
            try
            {
                if (IsOnline)
                {
                    StringContent stringContent = await Task.Run(
                        () => SerializeModel(parameters)
                    );

                    var response = await PostAsyncNoCache<GetRealTimePricingResult>(
                            CommerceAPIConstants.RealTimePricingUrl,
                            stringContent
                        );
                        
                    return response;
                }
                else
                {
                    return GetServiceResponse<GetRealTimePricingResult>();
                }
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<GetRealTimePricingResult>(exception: e);
            }
        }
    }
}
