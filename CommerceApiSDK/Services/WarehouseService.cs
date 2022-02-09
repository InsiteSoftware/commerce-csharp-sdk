﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WarehouseService : ServiceBase, IWarehouseService
    {
        private const string WarehousesUrl = "/api/v1/warehouses";

        public WarehouseService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
         : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<GetWarehouseCollectionResult> GetWarehouses(double latitude = 0, double longitude = 0, int pageNumber = 1, int pageSize = 16)
        {
            try
            {
                string url = WarehousesUrl;
                List<string> parameters = new List<string>()
                {
                    "latitude=" + latitude,
                    "longitude=" + longitude,
                    "page=" + pageNumber,
                    "pageSize=" + pageSize,
                    "sort=Distance",
                    "onlyPickupWarehouses=true",
                };

                url += "?" + string.Join("&", parameters);

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