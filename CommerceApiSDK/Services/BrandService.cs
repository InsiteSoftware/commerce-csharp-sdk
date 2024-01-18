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
        public BrandService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService
        )
            : base(clientService, networkService, trackingService, cacheService, loggerService) { }

        public async Task<ServiceResponse<BrandAlphabetResult>> GetAlphabetAsync()
        {
            try
            {
                return await GetAsyncWithCachedResponse<BrandAlphabetResult>(
                    CommerceAPIConstants.BrandAlphabetUrl
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<BrandAlphabetResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetBrandsResult>> GetBrands(
            BrandsQueryParameters parameters
        )
        {
            try
            {
                return await GetAsyncWithCachedResponse<GetBrandsResult>(
                    CommerceAPIConstants.BrandUrl + parameters.ToQueryString()
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetBrandsResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<Brand>> GetBrand(
            Guid brandId,
            BrandQueryParameters brandParameters = null
        )
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
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Brand>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetBrandCategoriesResult>> GetBrandCategories(
            BrandCategoriesQueryParameter parameters
        )
        {
            try
            {
                string url =
                    string.Format(CommerceAPIConstants.BrandCategoriesUrlFormat, parameters.BrandId)
                    + parameters.ToQueryString();
                return await GetAsyncWithCachedResponse<GetBrandCategoriesResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetBrandCategoriesResult>(exception: exception);
            }
        }

        public async Task<
            ServiceResponse<GetBrandSubCategoriesResult>
        > GetBrandCategorySubCategories(BrandCategoriesQueryParameter parameters)
        {
            try
            {
                string url = string.Format(
                    CommerceAPIConstants.BrandSubCategoriesUrlFormat,
                    parameters?.BrandId,
                    parameters?.CategoryId
                );
                var result = await GetAsyncWithCachedResponse<GetBrandSubCategoriesResult>(url);
                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetBrandSubCategoriesResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetBrandProductLinesResult>> GetBrandProductLines(
            ProductLinesQueryParameters parameters
        )
        {
            try
            {
                string url =
                    string.Format(
                        CommerceAPIConstants.BrandProductLinesUrlFormat,
                        parameters.BrandId
                    ) + parameters.ToQueryString();
                return await GetAsyncWithCachedResponse<GetBrandProductLinesResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetBrandProductLinesResult>(exception: exception);
            }
        }
    }
}
