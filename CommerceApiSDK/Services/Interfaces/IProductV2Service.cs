using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IProductV2Service
    {
        Task<ServiceResponse<GetProductCollectionResult>> GetProducts(ProductsQueryV2Parameters parameters);

        Task<ServiceResponse<GetProductResult>> GetProduct(
            Guid productId,
            ProductQueryV2Parameters parameters = null
        );

        Task<ServiceResponse<GetProductCollectionResult>> GetAlsoPurchased(
            Guid productId,
            AlsoPurchasedParameters parameters = null
        );

        Task<ServiceResponse<GetProductCollectionResult>> GetRelatedProduct(
            Guid productId,
            RelatedProductParameters parameters = null
        );

        Task<ServiceResponse<GetProductCollectionResult>> GetVariantChildren(
            Guid productId,
            VariantChildrenParameters parameters = null
        );

        Task<ServiceResponse<GetProductResult>> GetVariantChildrenDetail(
            Guid productId,
            Guid variantChildId,
            VariantChildrenDetailParameters parameters = null
        );
    }
}
