using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
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