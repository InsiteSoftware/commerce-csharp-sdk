using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class CatalogpagesService : ServiceBase, ICatalogpagesService
    {
        public CatalogpagesService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<CatalogpagesResult> GetProductCatalogInformation(string productPath)
        {
            try
            {
                string url = $"{CommerceAPIConstants.CatalogpageUrl}{productPath}";

                CatalogpagesResult productResult = await GetAsyncWithCachedResponse<CatalogpagesResult>(url);

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
