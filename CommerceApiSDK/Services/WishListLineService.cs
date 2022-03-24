using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WishListLineService : WishListServiceBase, IWishListLineService
    {
        public WishListLineService(
           ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
        {
        }

        public async Task<WishListLineCollectionModel> GetWishListLines(Guid wishListId, WishListLineQueryParameters parameters)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WishListUrl}/{wishListId}/wishlistlines";

                url += parameters?.ToQueryString();

                return await GetAsyncWithCachedResponse<WishListLineCollectionModel>(url);
            }
            catch (Exception e)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(e);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(e);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(e);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(e);
                return null;
            }
        }
    }
}
