using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetProductCollectionResult> GetProducts(ProductsQueryParameters parameters);

        Task<GetProductCollectionResult> GetProductsNoCache(ProductsQueryParameters parameters);

        Task<bool> HasProductsCache(ProductsQueryParameters parameters);

        Task<GetProductResult> GetProduct(Guid productId, ProductQueryParameters parameters = null);

        Task<ProductPriceDto> GetProductPrice(Guid productId, decimal quantity, string unitOfMeasure, List<Guid> configuration = null);

        Task<GetRealTimePricingResult> GetProductRealTimePrices(List<ProductPriceQueryParameter> productPriceParameters);

        Task<GetRealTimeInventoryResult> GetProductRealTimeInventory(RealTimeInventoryParameters parameters);
    }
}
