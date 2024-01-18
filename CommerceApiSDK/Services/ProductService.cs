using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<GetProductCollectionResult>> GetProducts(
            ProductsQueryParameters parameters
        )
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{CommerceAPIConstants.ProductsUrl}/{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);
                GetProductCollectionResult productsResult = response.Model;

                if (productsResult == null)
                {
                    return response;
                }

                foreach (Product product in productsResult.Products)
                {
                    FixProduct(product);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductCollectionResult>(exception: exception);
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<ServiceResponse<GetProductCollectionResult>> GetProductsNoCache(
            ProductsQueryParameters parameters
        )
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{CommerceAPIConstants.ProductsUrl}/{queryString}";

                var response = await GetAsyncNoCache<GetProductCollectionResult>(url);
                GetProductCollectionResult productsResult = response.Model;

                if (productsResult == null)
                {
                    return response;
                }

                foreach (Product product in productsResult.Products)
                {
                    FixProduct(product);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductCollectionResult>(exception: exception);
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<bool> HasProductsCache(ProductsQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.ProductsUrl + parameters.ToQueryString();
                string key = this.ClientService.Host + url + this.ClientService.SessionStateKey;
                bool result = await this.CacheService.HasOnlineCache(key);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<ServiceResponse<GetProductResult>> GetProduct(
            Guid productId,
            ProductQueryParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductResult>(url);

                GetProductResult productResult = response.Model;

                if (productResult == null)
                {
                    return response;
                }

                if (productResult.Product != null)
                {
                    FixProduct(productResult.Product);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetProductCollectionResult>> GetProductCrossSells(
            Guid productId
        )
        {
            try
            {
                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}/crosssells";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                GetProductCollectionResult productsResult = response.Model;

                if (productsResult == null)
                {
                    return response;
                }

                foreach (Product product in productsResult.Products)
                {
                    FixProduct(product);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductCollectionResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<ProductPrice>> GetProductPrice(
            Guid productId,
            ProductPriceQueryParameter parameters
        )
        {
            try
            {
                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}/price";

                if (parameters.Configuration.Count > 0)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var response = await GetAsyncWithCachedResponse<ProductPrice>(url);

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<ProductPrice>(exception: exception);
            }
        }

        private void FixProduct(Product product)
        {
            if (product.Pricing == null)
            {
                product.Pricing = new ProductPrice();
            }

            if (product.Availability == null)
            {
                product.Availability = new Availability();
            }
        }
    }
}
