namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    /// <summary>
    /// Service which retrieves categories from Insite api.
    /// </summary>
    public class CategoryService : ServiceBase, ICategoryService
    {
        private const string CategoryUrl = "/api/v1/categories";
        private const string MobileImageProperty = "mobileImage";
        private const string MobilePrimaryTextProperty = "mobilePrimaryText";
        private const string MobileSecondaryTextProperty = "mobileSecondaryText";

        private CategoryResult lastCategoryResult;

        public CategoryService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        /// <summary>
        /// Gets a list of categories
        /// </summary>
        /// <param name="startCategoryId">Parent category or null for base level categories.</param>
        /// <param name="maxDepth">depth of children to fetch.</param>
        /// <returns>List of categories.</returns>
        public async Task<List<Category>> GetCategoryList(Guid? startCategoryId = null, int? maxDepth = null)
        {
            try
            {
                var url = CategoryService.CategoryUrl;
                var parameters = new List<string>();
                if (startCategoryId.HasValue)
                {
                    parameters.Add("parameter.startCategoryId=" + startCategoryId);
                }

                if (maxDepth.HasValue)
                {
                    parameters.Add("parameter.maxDepth=" + maxDepth);
                }

                if (parameters.Count > 0)
                {
                    url += "?" + string.Join("&", parameters);
                }

                var categoryResult = await this.GetAsyncWithCachedResponse<CategoryResult>(url);
                if (categoryResult == null)
                {
                    return null;
                }

                this.lastCategoryResult = categoryResult;

                return categoryResult.Categories?.ToList();
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
                var url = CategoryUrl + "/" + categoryId;
                var response = await this.GetAsyncWithCachedResponse<Category>(url);
                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<List<Category>> GetFeaturedCategories(int? maxDepth = null)
        {
            try
            {
                var url = CategoryService.CategoryUrl;

                if (maxDepth.HasValue)
                {
                    url += "?maxDepth=" + maxDepth;
                }

                var allCategories = await this.GetAsyncWithCachedResponse<CategoryResult>(url);
                var flattedCategories = this.FlattenCategoryTree(allCategories.Categories);
                var featuredCategories = flattedCategories.Where(c => c.IsFeatured).ToList();

                return featuredCategories;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public List<Category> FlattenCategoryTree(IList<Category> categoryList)
        {
            var flattened = new List<Category>();

            foreach (var category in categoryList)
            {
                flattened.Add(category);

                if (category.SubCategories != null)
                {
                    flattened.AddRange(this.FlattenCategoryTree(category.SubCategories));
                }
            }

            return flattened;
        }

        public async Task<bool> HasCategoryCache(Guid categoryId)
        {
            var url = CategoryUrl + "/" + categoryId;
            return await this.HasCache(url);
        }
    }
}
