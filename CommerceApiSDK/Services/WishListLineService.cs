using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Services.Attributes;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WishListLineService : WishListServiceBase, IWishListLineService
    {
        public WishListLineService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<WishListLineCollectionModel> GetWishListLines(Guid wishListId, int pageNumber = 1, int pageSize = 16, WishListLineSortOrder sortOrder = WishListLineSortOrder.CustomSort, string searchQuery = null)
        {
            List<string> parameters = new List<string>()
            {
                "page=" + pageNumber,
                "pageSize=" + pageSize,
                "sort=" + SortOrderAttribute.GetSortOrderValue(sortOrder)
            };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                parameters.Add("query=" + WebUtility.UrlEncode(searchQuery));
            }

            string url = $"/api/v1/wishlists/{wishListId}/wishlistlines" + "?" + string.Join("&", parameters);

            try
            {
                return await GetAsyncWithCachedResponse<WishListLineCollectionModel>(url);
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId)
        {
            try
            {
                HttpResponseMessage response = await DeleteAsync($"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLineId}");
                bool result = response.IsSuccessStatusCode;

                if (result)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<bool> DeleteWishListLineCollection(Guid wishListId, IList<WishListLine> wishListLineCollection)
        {
            if (wishListLineCollection == null || wishListLineCollection.Count <= 0)
            {
                return false;
            }

            string queryString = "?" + string.Join("&", wishListLineCollection.Select(o => $"wishListLineIds={o.Id}"));

            try
            {
                HttpResponseMessage response = await DeleteAsync($"/api/v1/wishlists/{wishListId}/wishlistlines/batch" + queryString);
                bool result = response.IsSuccessStatusCode;

                if (result)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<WishListLine> UpdateWishListLine(Guid wishListId, WishListLine wishListLine)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishListLine));
            try
            {
                WishListLine result = await PatchAsyncNoCache<WishListLine>($"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLine.Id}", stringContent);

                if (result != null)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }
    }
}
