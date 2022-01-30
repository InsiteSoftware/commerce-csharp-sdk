namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;

    public interface IBrandService
    {
        Task<BrandAlphabetResult> GetAlphabetAsync();

        Task<GetBrandsResult> GetBrands(BrandsQueryParameters parameters);

        Task<Brand> GetBrand(Guid brandId, BrandQueryParameters brandParameters = null);

        Task<GetBrandCategoriesResult> GetBrandCategories(BrandCategoriesQueryParameter parameters);

        Task<GetBrandSubCategoriesResult> GetBrandCategorySubCategories(BrandCategoriesQueryParameter parameters);

        Task<GetBrandProductLinesResult> GetBrandProductLines(ProductLinesQueryParameters parameters);
    }
}