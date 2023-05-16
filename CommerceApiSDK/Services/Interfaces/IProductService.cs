using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<GetProductCollectionResult>> GetProducts(ProductsQueryParameters parameters);

        Task<ServiceResponse<GetProductCollectionResult>> GetProductsNoCache(ProductsQueryParameters parameters);

        Task<bool> HasProductsCache(ProductsQueryParameters parameters);

        Task<ServiceResponse<GetProductResult>> GetProduct(Guid productId, ProductQueryParameters parameters = null);

        Task<ServiceResponse<GetProductCollectionResult>> GetProductCrossSells(Guid productId);

        Task<ServiceResponse<ProductPrice>> GetProductPrice(Guid productId, ProductPriceQueryParameter parameters);
    }
}
