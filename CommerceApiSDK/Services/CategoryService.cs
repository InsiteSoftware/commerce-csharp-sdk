using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    /// <summary>
    /// Service which retrieves categories from Insite api.
    /// </summary>
    public class CategoryService : ServiceBase, ICategoryService
    {
        private CategoryResult lastCategoryResult;

        public CategoryService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        /// <summary>
        /// Gets a list of categories
        /// </summary>
        /// <param name="startCategoryId">Parent category or null for base level categories.</param>
        /// <param name="maxDepth">depth of children to fetch.</param>
        /// <returns>List of categories.</returns>
        public async Task<List<Category>> GetCategoryList(CategoryQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                CategoryResult categoryResult = await GetAsyncWithCachedResponse<CategoryResult>(url);
                if (categoryResult == null)
                {
                    return null;
                }

                lastCategoryResult = categoryResult;

                return categoryResult.Categories?.ToList();
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        /// <summary>
        /// Gets a single category
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>The category.</returns>
        public async Task<Category> GetCategory(Guid categoryId)
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl + "/" + categoryId;
                Category response = await GetAsyncWithCachedResponse<Category>(url);
                return response;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<List<Category>> GetFeaturedCategories(FeaturedCategoryQueryParameter parameter)
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl;

                if (parameter != null)
                {
                    string queryString = parameter.ToQueryString();
                    url += queryString;
                }

                CategoryResult allCategories = await GetAsyncWithCachedResponse<CategoryResult>(url);
                List<Category> flattedCategories = FlattenCategoryTree(allCategories.Categories);
                List<Category> featuredCategories = flattedCategories.Where(c => c.IsFeatured).ToList();

                return featuredCategories;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public List<Category> FlattenCategoryTree(IList<Category> categoryList)
        {
            List<Category> flattened = new List<Category>();

            foreach (Category category in categoryList)
            {
                flattened.Add(category);

                if (category.SubCategories != null)
                {
                    flattened.AddRange(FlattenCategoryTree(category.SubCategories));
                }
            }

            return flattened;
        }

        public async Task<bool> HasCategoryCache(Guid categoryId)
        {
            string url = CommerceAPIConstants.CategoryUrl + "/" + categoryId;
            return await HasCache(url);
        }
    }
}
