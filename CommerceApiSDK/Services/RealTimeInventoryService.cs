using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class RealTimeInventoryService : ServiceBase, IRealTimeInventoryService
    {
        public RealTimeInventoryService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<GetRealTimeInventoryResult> GetProductRealTimeInventory(
            RealTimeInventoryParameters parameters
        )
        {
            try
            {
                if (IsOnline)
                {
                    string queryString = string.Empty;

                    if (parameters != null)
                    {
                        queryString = parameters.ToQueryString();
                    }

                    string url = $"{CommerceAPIConstants.RealTimeInventoryUrl}/{queryString}";

                    StringContent stringContent = await Task.Run(
                        () => SerializeModel(new { parameters.ProductIds })
                    );

                    GetRealTimeInventoryResult result =
                        await PostAsyncNoCache<GetRealTimeInventoryResult>(url, stringContent);

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
