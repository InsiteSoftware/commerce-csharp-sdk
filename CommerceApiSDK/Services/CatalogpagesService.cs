using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class CatalogpagesService : ServiceBase, ICatalogpagesService
    {
        public CatalogpagesService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<CatalogPage> GetProductCatalogInformation(string productPath)
        {
            try
            {
                string url = $"{CommerceAPIConstants.CatalogpageUrl}{productPath}";

                CatalogPage productResult = await GetAsyncWithCachedResponse<CatalogPage>(url);

                if (productResult == null)
                {
                    return null;
                }

                return productResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
