namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;

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
        Task<List<Category>> GetCategoryList(Guid? startCategoryId = null, int? maxDepth = null);

        Task<Category> GetCategory(Guid categoryId);

        Task<bool> HasCategoryCache(Guid categoryId);

        Task<List<Category>> GetFeaturedCategories(int? maxDepth = null);
    }
}
