using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class BrandService : ServiceBase, IBrandService
    {
        public BrandService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<BrandAlphabetResult> GetAlphabetAsync()
        {
            try
            {
                return await GetAsyncNoCache<BrandAlphabetResult>(CommerceAPIConstants.BrandAlphabetUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandsResult> GetBrands(BrandsQueryParameters parameters)
        {
            try
            {
                return await GetAsyncNoCache<GetBrandsResult>(CommerceAPIConstants.BrandUrl + parameters.ToQueryString());
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Brand> GetBrand(Guid brandId, BrandQueryParameters brandParameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (brandParameters != null)
                {
                    queryString = brandParameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.BrandUrl}/{brandId}{queryString}";
                return await GetAsyncWithCachedResponse<Brand>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandCategoriesResult> GetBrandCategories(BrandCategoriesQueryParameter parameters)
        {
            try
            {
                string url = string.Format(CommerceAPIConstants.BrandCategoriesUrlFormat, parameters.BrandId) + parameters.ToQueryString();
                return await GetAsyncWithCachedResponse<GetBrandCategoriesResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandSubCategoriesResult> GetBrandCategorySubCategories(BrandCategoriesQueryParameter parameters)
        {
            try
            {
                string url = string.Format(CommerceAPIConstants.BrandSubCategoriesUrlFormat, parameters?.BrandId, parameters?.CategoryId);
                GetBrandSubCategoriesResult result = await GetAsyncWithCachedResponse<GetBrandSubCategoriesResult>(url);
                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandProductLinesResult> GetBrandProductLines(ProductLinesQueryParameters parameters)
        {
            try
            {
                string url = string.Format(CommerceAPIConstants.BrandProductLinesUrlFormat, parameters.BrandId) + parameters.ToQueryString();
                return await GetAsyncWithCachedResponse<GetBrandProductLinesResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}