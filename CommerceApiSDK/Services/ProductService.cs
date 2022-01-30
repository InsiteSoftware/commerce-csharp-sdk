namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class ProductService : ServiceBase, IProductService
    {
        private const string ProductsUrl = "/api/v1/products";
        private const string MainProductUrl = "/api/v1/catalogpages?path=";
        private const string RealTimePricingUrl = "/api/v1/realtimepricing";
        private const string RealTimeInventoryUrl = "/api/v1/realtimeinventory";

        public ProductService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<GetProductCollectionResult> GetProducts(ProductsQueryParameters parameters)
        {
            try
            {
                var queryString = parameters.ToQueryString();
                var url = $"{ProductsUrl}/{queryString}";

                var productsResult = await this.GetAsyncWithCachedResponse<GetProductCollectionResult>(url);

                if (productsResult == null)
                {
                    return null;
                }

                foreach (var product in productsResult.Products)
                {
                    this.FixProduct(product);
                }

                return productsResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetProductsNoCache(ProductsQueryParameters parameters)
        {
            try
            {
                var queryString = parameters.ToQueryString();
                var url = $"{ProductsUrl}/{queryString}";

                var productsResult = await this.GetAsyncNoCache<GetProductCollectionResult>(url);

                if (productsResult == null)
                {
                    return null;
                }

                foreach (var product in productsResult.Products)
                {
                    this.FixProduct(product);
                }

                return productsResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> HasProductsCache(ProductsQueryParameters parameters)
        {
            try
            {
                var url = ProductsUrl + parameters.ToQueryString();

                var result = await this.HasCache(url);

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
                var queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                var url = $"{ProductsUrl}/{productId}{queryString}";

                var productResult = await this.GetAsyncWithCachedResponse<GetProductResult>(url);

                if (productResult == null)
                {
                    return null;
                }

                if (productResult.Product != null)
                {
                    this.FixProduct(productResult.Product);
                }

                return productResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetCatalogPageResult> GetProductCatalogInformation(string productPath)
        {
            try
            {
                var url = $"{MainProductUrl}{productPath}";

                var productResult = await this.GetAsyncWithCachedResponse<GetCatalogPageResult>(url);

                if (productResult == null)
                {
                    return null;
                }

                return productResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductPriceDto> GetProductPrice(Guid productId, decimal quantity, string unitOfMeasure, List<Guid> configuration = null)
        {
            try
            {
                var url = $"{ProductsUrl}/{productId}/price?qtyOrdered={quantity}&unitOfMeasure={unitOfMeasure}";

                if (configuration != null && configuration.Count != 0)
                {
                    url += "&configuration=" + string.Join("&configuration=", configuration);
                }

                var pricingResult = await this.GetAsyncWithCachedResponse<ProductPriceDto>(url);

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
                product.Pricing = new ProductPriceDto();
            }

            if (product.Availability == null)
            {
                product.Availability = new AvailabilityDto();
            }
        }

        public async Task<GetRealTimePricingResult> GetProductRealTimePrices(List<ProductPriceQueryParameter> productPriceParameters)
        {
            try
            {
                if (this.IsOnline)
                {
                    var stringContent = await Task.Run(() => ServiceBase.SerializeModel(new { productPriceParameters }));
                    var result = await this.PostAsyncNoCache<GetRealTimePricingResult>(RealTimePricingUrl, stringContent);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<GetRealTimeInventoryResult> GetProductRealTimeInventory(RealTimeInventoryParameters parameters)
        {
            try
            {
                if (this.IsOnline)
                {
                    var queryString = string.Empty;

                    if (parameters != null)
                    {
                        queryString = parameters.ToQueryString();
                    }

                    var url = $"{RealTimeInventoryUrl}/{queryString}";

                    var stringContent = await Task.Run(() => ServiceBase.SerializeModel(new { parameters.ProductIds }));

                    var result = await this.PostAsyncNoCache<GetRealTimeInventoryResult>(url, stringContent);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }
    }
}
