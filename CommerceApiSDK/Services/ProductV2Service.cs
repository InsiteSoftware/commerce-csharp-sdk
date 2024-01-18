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
        public ProductV2Service(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

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

        public async Task<ServiceResponse<GetProductCollectionResult>> GetProducts(
            ProductsQueryV2Parameters parameters
        )
        {
            if (parameters == null)
            {
                throw new ArgumentException("Parameters is empty");
            }

            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{CommerceAPIConstants.ProductsV2Url}/{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);
                GetProductCollectionResult result = response.Model;

                if (result == null)
                {
                    return response;
                }

                foreach (Product product in result.Products)
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

        public async Task<ServiceResponse<GetProductResult>> GetProduct(
            Guid productId,
            ProductQueryV2Parameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.ProductsV2Url}/{productId}{queryString}";

                var response = await GetAsyncWithCachedResponse<Product>(url);
                GetProductResult result = new GetProductResult { Product = response.Model };

                if (result == null)
                {
                    return GetServiceResponse<GetProductResult>(
                        error: response.Error,
                        exception: response.Exception,
                        statusCode: response.StatusCode,
                        isCached: response.IsCached
                    );
                }

                if (result.Product != null)
                {
                    FixProduct(result.Product);
                }

                return GetServiceResponse<GetProductResult>(
                    model: result,
                    error: response.Error,
                    exception: response.Exception,
                    statusCode: response.StatusCode,
                    isCached: response.IsCached
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetProductCollectionResult>> GetAlsoPurchased(
            Guid productId,
            AlsoPurchasedParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url =
                    $"{CommerceAPIConstants.ProductsV2Url}/{productId}/alsopurchased{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);
                GetProductCollectionResult result = response.Model;

                if (result == null)
                {
                    return response;
                }

                foreach (Product product in result.Products)
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

        public async Task<ServiceResponse<GetProductCollectionResult>> GetRelatedProduct(
            Guid productId,
            RelatedProductParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url =
                    $"{CommerceAPIConstants.ProductsV2Url}/{productId}/relatedproducts{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);
                GetProductCollectionResult result = response.Model;

                if (result == null)
                {
                    return response;
                }

                foreach (Product product in result.Products)
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

        public async Task<ServiceResponse<GetProductCollectionResult>> GetVariantChildren(
            Guid productId,
            VariantChildrenParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url =
                    $"{CommerceAPIConstants.ProductsV2Url}/{productId}/variantchildren{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductCollectionResult>(url);
                GetProductCollectionResult result = response.Model;

                if (result == null)
                {
                    return response;
                }

                foreach (Product product in result.Products)
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

        public async Task<ServiceResponse<GetProductResult>> GetVariantChildrenDetail(
            Guid productId,
            Guid variantChildId,
            VariantChildrenDetailParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url =
                    $"{CommerceAPIConstants.ProductsV2Url}/{productId}/variantchildren/{variantChildId}{queryString}";

                var response = await GetAsyncWithCachedResponse<GetProductResult>(url);
                GetProductResult result = response.Model;

                if (result == null)
                {
                    return response;
                }

                if (result.Product != null)
                {
                    FixProduct(result.Product);
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductResult>(exception: exception);
            }
        }
    }
}
