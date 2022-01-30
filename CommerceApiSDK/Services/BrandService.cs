namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class BrandService : ServiceBase, IBrandService
    {
        private const string BrandAlphabetUrl = "/api/v1/brandalphabet";
        private const string BrandUrl = "/api/v1/brands";
        private const string BrandCategoriesUrlFormat = "/api/v1/brands/{0}/categories";
        private const string BrandSubCategoriesUrlFormat = "/api/v1/brands/{0}/categories/{1}";
        private const string BrandProductLinesUrlFormat = "/api/v1/brands/{0}/productlines";

        public BrandService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<BrandAlphabetResult> GetAlphabetAsync()
        {
            try
            {
                return await this.GetAsyncNoCache<BrandAlphabetResult>(BrandAlphabetUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandsResult> GetBrands(BrandsQueryParameters parameters)
        {
            try
            {
                return await this.GetAsyncNoCache<GetBrandsResult>(BrandUrl + parameters.ToQueryString());
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Brand> GetBrand(Guid brandId, BrandQueryParameters brandParameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (brandParameters != null)
                {
                    queryString = brandParameters.ToQueryString();
                }

                var url = $"{BrandUrl}/{brandId}{queryString}";
                return await this.GetAsyncWithCachedResponse<Brand>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandCategoriesResult> GetBrandCategories(BrandCategoriesQueryParameter parameters)
        {
            try
            {
                var url = string.Format(BrandCategoriesUrlFormat, parameters.BrandId) + parameters.ToQueryString();
                return await this.GetAsyncWithCachedResponse<GetBrandCategoriesResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandSubCategoriesResult> GetBrandCategorySubCategories(BrandCategoriesQueryParameter parameters)
        {
            try
            {
                var url = string.Format(BrandSubCategoriesUrlFormat, parameters?.BrandId, parameters?.CategoryId);
                var result = await this.GetAsyncWithCachedResponse<GetBrandSubCategoriesResult>(url);
                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetBrandProductLinesResult> GetBrandProductLines(ProductLinesQueryParameters parameters)
        {
            try
            {
                var url = string.Format(BrandProductLinesUrlFormat, parameters.BrandId) + parameters.ToQueryString();
                return await this.GetAsyncWithCachedResponse<GetBrandProductLinesResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}