namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

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
                var queryString = parameters.ToQueryString();
                var url = $"{ProductsUrl}/{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (var product in result.Products)
                {
                    this.FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductResult> GetProduct(Guid productId, ProductQueryV2Parameters parameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductResult>(url);

                if (result == null)
                {
                    return null;
                }

                if (result.Product != null)
                {
                    this.FixProduct(result.Product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetAlsoPurchased(Guid productId, AlsoPurchasedParameters parameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}/alsopurchased{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (var product in result.Products)
                {
                    this.FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetRelatedProduct(Guid productId, RelatedProductParameters parameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}/relatedproducts{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (var product in result.Products)
                {
                    this.FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetVariantChildren(Guid productId, VariantChildrenParameters parameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}/variantchildren{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (result == null)
                {
                    return null;
                }

                foreach (var product in result.Products)
                {
                    this.FixProduct(product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductResult> GetVariantChildrenDetail(Guid productId, Guid variantChildId, VariantChildrenDetailParameters parameters = null)
        {
            try
            {
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}/variantchildren/{variantChildId}{queryString}";

                var result = await this.GetAsyncWithCachedResponse<GetProductResult>(url);

                if (result == null)
                {
                    return null;
                }

                if (result.Product != null)
                {
                    this.FixProduct(result.Product);
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
