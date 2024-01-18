using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class WarehouseService : ServiceBase, IWarehouseService
    {
        public WarehouseService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<GetWarehouseCollectionResult>> GetWarehouses(
            WarehousesQueryParameters parameters
        )
        {
            try
            {
                string url = CommerceAPIConstants.WarehousesUrl;

                url += parameters?.ToQueryString();

                return await GetAsyncWithCachedResponse<GetWarehouseCollectionResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetWarehouseCollectionResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<Warehouse>> GetWarehouse(
            Guid warehouseId,
            WarehouseQueryParameters parameters
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.WarehousesUrl}/{warehouseId}{queryString}";

                var warehouseResult = await GetAsyncWithCachedResponse<Warehouse>(url);

                return warehouseResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Warehouse>(exception: exception);
            }
        }
    }
}
