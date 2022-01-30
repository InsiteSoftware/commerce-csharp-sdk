namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;

    public interface IProductV2Service
    {
        Task<GetProductCollectionResult> GetProducts(ProductsQueryV2Parameters parameters);

        Task<GetProductResult> GetProduct(Guid productId, ProductQueryV2Parameters parameters = null);

        Task<GetProductCollectionResult> GetAlsoPurchased(Guid productId, AlsoPurchasedParameters parameters = null);

        Task<GetProductCollectionResult> GetRelatedProduct(Guid productId, RelatedProductParameters parameters = null);

        Task<GetProductCollectionResult> GetVariantChildren(Guid productId, VariantChildrenParameters parameters = null);

        Task<GetProductResult> GetVariantChildrenDetail(Guid productId, Guid variantChildId, VariantChildrenDetailParameters parameters = null);
    }
}
