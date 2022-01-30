namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Attributes;

    public interface IProductService
    {
        Task<GetProductCollectionResult> GetProducts(ProductsQueryParameters parameters);

        Task<GetProductCollectionResult> GetProductsNoCache(ProductsQueryParameters parameters);

        Task<bool> HasProductsCache(ProductsQueryParameters parameters);

        Task<GetProductResult> GetProduct(Guid productId, ProductQueryParameters parameters = null);

        Task<GetCatalogPageResult> GetProductCatalogInformation(string productPath);

        Task<ProductPriceDto> GetProductPrice(Guid productId, decimal quantity, string unitOfMeasure, List<Guid> configuration = null);

        Task<GetRealTimePricingResult> GetProductRealTimePrices(List<ProductPriceQueryParameter> productPriceParameters);

        Task<GetRealTimeInventoryResult> GetProductRealTimeInventory(RealTimeInventoryParameters parameters);
    }
}
