using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class CatalogpagesService : ServiceBase, ICatalogpagesService
    {
        public CatalogpagesService(IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
            : base(optiAPIBaseServiceProvider)
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
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}
