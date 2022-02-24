using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private const string ProductsUrl = "/api/v1/products";
        private const string MainProductUrl = "/api/v1/catalogpages?path=";
        private const string RealTimePricingUrl = "/api/v1/realtimepricing";
        private const string RealTimeInventoryUrl = "/api/v1/realtimeinventory";

        public ProductService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<GetProductCollectionResult> GetProducts(ProductsQueryParameters parameters)
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{ProductsUrl}/{queryString}";

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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetProductCollectionResult> GetProductsNoCache(ProductsQueryParameters parameters)
        {
            try
            {
                string queryString = parameters.ToQueryString();
                string url = $"{ProductsUrl}/{queryString}";

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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> HasProductsCache(ProductsQueryParameters parameters)
        {
            try
            {
                string url = ProductsUrl + parameters.ToQueryString();

                bool result = await HasCache(url);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{ProductsUrl}/{productId}{queryString}";

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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetCatalogPageResult> GetProductCatalogInformation(string productPath)
        {
            try
            {
                string url = $"{MainProductUrl}{productPath}";

                GetCatalogPageResult productResult = await GetAsyncWithCachedResponse<GetCatalogPageResult>(url);

                if (productResult == null)
                {
                    return null;
                }

                return productResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductPriceDto> GetProductPrice(Guid productId, decimal quantity, string unitOfMeasure, List<Guid> configuration = null)
        {
            try
            {
                string url = $"{ProductsUrl}/{productId}/price?qtyOrdered={quantity}&unitOfMeasure={unitOfMeasure}";

                if (configuration != null && configuration.Count != 0)
                {
                    url += "&configuration=" + string.Join("&configuration=", configuration);
                }

                ProductPriceDto pricingResult = await GetAsyncWithCachedResponse<ProductPriceDto>(url);

                return pricingResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                if (IsOnline)
                {
                    StringContent stringContent = await Task.Run(() => SerializeModel(new { productPriceParameters }));
                    GetRealTimePricingResult result = await PostAsyncNoCache<GetRealTimePricingResult>(RealTimePricingUrl, stringContent);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<GetRealTimeInventoryResult> GetProductRealTimeInventory(RealTimeInventoryParameters parameters)
        {
            try
            {
                if (IsOnline)
                {
                    string queryString = string.Empty;

                    if (parameters != null)
                    {
                        queryString = parameters.ToQueryString();
                    }

                    string url = $"{RealTimeInventoryUrl}/{queryString}";

                    StringContent stringContent = await Task.Run(() => SerializeModel(new { parameters.ProductIds }));

                    GetRealTimeInventoryResult result = await PostAsyncNoCache<GetRealTimeInventoryResult>(url, stringContent);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }
    }
}
