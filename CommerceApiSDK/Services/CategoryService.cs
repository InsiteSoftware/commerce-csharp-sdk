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

        public CategoryService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        /// <summary>
        /// Gets a list of categories
        /// </summary>
        /// <param name="startCategoryId">Parent category or null for base level categories.</param>
        /// <param name="maxDepth">depth of children to fetch.</param>
        /// <returns>List of categories.</returns>
        public async Task<ServiceResponse<List<Category>>> GetCategoryList(
            CategoryQueryParameters parameters
        )
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl;

                url += parameters?.ToQueryString();

                var categoryResult = await GetAsyncWithCachedResponse<CategoryResult>(url);

                lastCategoryResult = categoryResult?.Model;

                return new ServiceResponse<List<Category>>()
                {
                    Model = categoryResult.Model?.Categories?.ToList(),
                    Error = categoryResult.Error,
                    Exception = categoryResult.Exception,
                    StatusCode = categoryResult.StatusCode,
                    IsCached = categoryResult.IsCached
                };
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<List<Category>>(exception: exception);
            }
        }

        /// <summary>
        /// Gets a single category
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>The category.</returns>
        public async Task<ServiceResponse<Category>> GetCategory(Guid categoryId)
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl + "/" + categoryId;
                var response = await GetAsyncWithCachedResponse<Category>(url);
                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Category>(exception: exception);
            }
        }

        public async Task<ServiceResponse<List<Category>>> GetFeaturedCategories(
            CategoryQueryParameters parameters
        )
        {
            try
            {
                string url = CommerceAPIConstants.CategoryUrl;

                url += parameters?.ToQueryString();

                var response = await GetAsyncWithCachedResponse<CategoryResult>(url);
                List<Category> featuredCategories = FlattenCategoryTree(response.Model?.Categories)
                    .Where(c => c.IsFeatured)
                    .ToList();

                return new ServiceResponse<List<Category>>()
                {
                    Model = featuredCategories,
                    Error = response.Error,
                    Exception = response.Exception,
                    StatusCode = response.StatusCode,
                    IsCached = response.IsCached
                };
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<List<Category>>(exception: exception);
            }
        }

        private List<Category> FlattenCategoryTree(IList<Category> categoryList)
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
            string key = this.ClientService.Host + url + this.ClientService.SessionStateKey;
            return await this.CacheService.HasOnlineCache(key);
        }
    }
}
