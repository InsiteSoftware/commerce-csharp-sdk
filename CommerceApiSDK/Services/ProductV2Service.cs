using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class ProductV2Service : ServiceBase, IProductV2Service
    {
        private const string ProductsUrl = "/api/v2/products";

        public ProductV2Service(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService)
        {
        }

        private void FixProduct(Product product)
        {
            if (product.Pricing == null)
            {
                product.Pricing = new ProductPriceDto();
            }

            if (product.Availability == null)
            {
                product.Availability = new AvailabilityDto();
            }
        }

        public async Task<GetProductCollectionResult> GetProducts(ProductsQueryV2Parameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentException("Parameters is empty");
            }

            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{ProductsUrl}/{queryString}";

                GetProductCollectionResult result = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (Product product in result.Products)
                {
                    FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductResult> GetProduct(Guid productId, ProductQueryV2Parameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{ProductsUrl}/{productId}{queryString}";

                GetProductResult result = await GetAsyncWithCachedResponse<GetProductResult>(url);

                if (result == null)
                {
                    return null;
                }

                if (result.Product != null)
                {
                    FixProduct(result.Product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetAlsoPurchased(Guid productId, AlsoPurchasedParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{ProductsUrl}/{productId}/alsopurchased{queryString}";

                GetProductCollectionResult result = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (Product product in result.Products)
                {
                    FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetRelatedProduct(Guid productId, RelatedProductParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{ProductsUrl}/{productId}/relatedproducts{queryString}";

                GetProductCollectionResult result = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (Product product in result.Products)
                {
                    FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetVariantChildren(Guid productId, VariantChildrenParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{ProductsUrl}/{productId}/variantchildren{queryString}";

                GetProductCollectionResult result = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (Product product in result.Products)
                {
                    FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductResult> GetVariantChildrenDetail(Guid productId, Guid variantChildId, VariantChildrenDetailParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{ProductsUrl}/{productId}/variantchildren/{variantChildId}{queryString}";

                GetProductResult result = await GetAsyncWithCachedResponse<GetProductResult>(url);

                if (result == null)
                {
                    return null;
                }

                if (result.Product != null)
                {
                    FixProduct(result.Product);
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
