namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Services.Attributes;
    using CommerceApiSDK.Services.Interfaces;

    public class WishListLineService : WishListServiceBase, IWishListLineService
    {
        public WishListLineService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<WishListLineCollectionModel> GetWishListLines(Guid wishListId, int pageNumber = 1, int pageSize = 16, WishListLineSortOrder sortOrder = WishListLineSortOrder.CustomSort, string searchQuery = null)
        {
            var parameters = new List<string>
            {
                "page=" + pageNumber,
                "pageSize=" + pageSize,
                "sort=" + SortOrderAttribute.GetSortOrderValue(sortOrder)
            };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                parameters.Add("query=" + WebUtility.UrlEncode(searchQuery));
            }

            var url = $"/api/v1/wishlists/{wishListId}/wishlistlines" + "?" + string.Join("&", parameters);

            try
            {
                return await this.GetAsyncWithCachedResponse<WishListLineCollectionModel>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId)
        {
            try
            {
                var response = await this.DeleteAsync($"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLineId}");
                var result = response.IsSuccessStatusCode;

                if (result)
                {
                    await this.ClearWishListRelatedCacheAsync(wishListId);
                    await this.ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<bool> DeleteWishListLineCollection(Guid wishListId, IList<WishListLine> wishListLineCollection)
        {
            if (wishListLineCollection == null || wishListLineCollection.Count <= 0)
            {
                return false;
            }

            var queryString = "?" + string.Join("&", wishListLineCollection.Select(o => $"wishListLineIds={o.Id}"));

            try
            {
                var response = await this.DeleteAsync($"/api/v1/wishlists/{wishListId}/wishlistlines/batch" + queryString);
                var result = response.IsSuccessStatusCode;

                if (result)
                {
                    await this.ClearWishListRelatedCacheAsync(wishListId);
                    await this.ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<WishListLine> UpdateWishListLine(Guid wishListId, WishListLine wishListLine)
        {
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(wishListLine));
            try
            {
                var result = await this.PatchAsyncNoCache<WishListLine>($"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLine.Id}", stringContent);

                if (result != null)
                {
                    await this.ClearWishListRelatedCacheAsync(wishListId);
                    await this.ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }
    }
}
