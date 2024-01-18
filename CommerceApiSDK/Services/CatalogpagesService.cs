using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class CatalogpagesService : ServiceBase, ICatalogpagesService
    {
        public CatalogpagesService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<CatalogPage>> GetProductCatalogInformation(
            string productPath
        )
        {
            try
            {
                string url = $"{CommerceAPIConstants.CatalogPageUrl}{productPath}";

                var productResult = await GetAsyncWithCachedResponse<CatalogPage>(url);

                return productResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<CatalogPage>(exception: exception);
            }
        }
    }
}
