using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// Service which retrieves categories from Insite api.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Get a list of categories
        /// </summary>
        /// <param name="startCategoryId">Parent category or null for base level categories.</param>
        /// <param name="maxDepth">depth of children to fetch.</param>
        /// <returns>List of categories.</returns>
        Task<ServiceResponse<List<Category>>> GetCategoryList(CategoryQueryParameters parameters);

        Task<ServiceResponse<Category>> GetCategory(Guid categoryId);

        Task<bool> HasCategoryCache(Guid categoryId);

        Task<ServiceResponse<List<Category>>> GetFeaturedCategories(
            CategoryQueryParameters parameters
        );
    }
}
