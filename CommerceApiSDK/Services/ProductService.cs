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
            ILoggerService LoggerService)
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService)
        {
        }

        public async Task<GetProductCollectionResult> GetProducts(ProductsQueryParameters parameters)
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{CommerceAPIConstants.ProductsUrl}/{queryString}";

                GetProductCollectionResult productsResult = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (productsResult == null)
                {
                    return null;
                }

                foreach (Product product in productsResult.Products)
                {
                    FixProduct(product);
                }

                return productsResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<GetProductCollectionResult> GetProductsNoCache(ProductsQueryParameters parameters)
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{CommerceAPIConstants.ProductsUrl}/{queryString}";

                GetProductCollectionResult productsResult = await GetAsyncNoCache<GetProductCollectionResult>(url);

                if (productsResult == null)
                {
                    return null;
                }

                foreach (Product product in productsResult.Products)
                {
                    FixProduct(product);
                }

                return productsResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
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

        public async Task<GetProductResult> GetProduct(Guid productId, ProductQueryParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}{queryString}";

                GetProductResult productResult = await GetAsyncWithCachedResponse<GetProductResult>(url);

                if (productResult == null)
                {
                    return null;
                }

                if (productResult.Product != null)
                {
                    FixProduct(productResult.Product);
                }

                return productResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductPrice> GetProductPrice(Guid productId, ProductPriceQueryParameters parameters)
        {
            try
            {
                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}/price";

                if (parameters.Configuration.Count > 0)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                ProductPrice pricingResult = await GetAsyncWithCachedResponse<ProductPrice>(url);

                return pricingResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
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
