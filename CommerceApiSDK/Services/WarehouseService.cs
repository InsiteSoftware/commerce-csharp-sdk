using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
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

        public async Task<GetWarehouseCollectionResult> GetWarehouses(WarehousesQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.WarehousesUrl;

                url += parameters?.ToQueryString();

                return await GetAsyncWithCachedResponse<GetWarehouseCollectionResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Warehouse> GetWarehouse(Guid warehouseId, WarehouseQueryParameters parameters)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.WarehousesUrl}/{warehouseId}{queryString}";

                Warehouse warehouseResult = await GetAsyncWithCachedResponse<Warehouse>(url);

                if (warehouseResult == null)
                {
                    return null;
                }

                return warehouseResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
