using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IBrandService
    {
        Task<ServiceResponse<BrandAlphabetResult>> GetAlphabetAsync();

        Task<ServiceResponse<GetBrandsResult>> GetBrands(BrandsQueryParameters parameters);

        Task<ServiceResponse<Brand>> GetBrand(
            Guid brandId,
            BrandQueryParameters brandParameters = null
        );

        Task<ServiceResponse<GetBrandCategoriesResult>> GetBrandCategories(
            BrandCategoriesQueryParameter parameters
        );

        Task<ServiceResponse<GetBrandSubCategoriesResult>> GetBrandCategorySubCategories(
            BrandCategoriesQueryParameter parameters
        );

        Task<ServiceResponse<GetBrandProductLinesResult>> GetBrandProductLines(
            ProductLinesQueryParameters parameters
        );
    }
}
