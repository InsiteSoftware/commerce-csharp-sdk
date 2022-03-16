using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WarehouseService : ServiceBase, IWarehouseService
    {
        public WarehouseService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
         : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<GetWarehouseCollectionResult> GetWarehouses(WarehouseQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.WarehousesUrl;

                if(parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                return await GetAsyncWithCachedResponse<GetWarehouseCollectionResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
