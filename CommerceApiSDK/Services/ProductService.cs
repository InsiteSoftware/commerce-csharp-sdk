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
        public ProductService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
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
                TrackingService.TrackException(exception);
                return null;
            }
        }

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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> HasProductsCache(ProductsQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.ProductsUrl + parameters.ToQueryString();

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
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductPriceDto> GetProductPrice(Guid productId, ProductPriceQueryParameters parameters)
        {
            try
            {
                string url = $"{CommerceAPIConstants.ProductsUrl}/{productId}/price";

                if (parameters.Configuration.Count > 0)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
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
                    GetRealTimePricingResult result = await PostAsyncNoCache<GetRealTimePricingResult>(CommerceAPIConstants.RealTimePricingUrl, stringContent);
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

                    string url = $"{CommerceAPIConstants.RealTimeInventoryUrl}/{queryString}";

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
