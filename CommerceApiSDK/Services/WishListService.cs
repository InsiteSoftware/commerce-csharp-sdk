namespace CommerceApiSDK.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Attributes;
    using CommerceApiSDK.Services.Interfaces;

    public class WishListService : WishListServiceBase, IWishListService
    {
        public WishListService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<WishListCollectionModel> GetWishLists(int pageNumber = 1, int pageSize = 16, WishListSortOrder sortOrder = WishListSortOrder.ModifiedOnDescending, string searchText = null)
        {
            var url = this.GetWishListsUrl(pageNumber, pageSize, sortOrder, searchText);

            try
            {
                return await this.GetAsyncWithCachedResponse<WishListCollectionModel>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<WishList> GetWishList(Guid wishListId)
        {
            var url = this.GetWishListUrl(wishListId);

            try
            {
                return await this.GetAsyncWithCachedResponse<WishList>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<bool> DeleteWishList(Guid wishListId)
        {
            try
            {
                var response = await this.DeleteAsync("/api/v1/wishlists/" + wishListId);
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

        public async Task<WishList> CreateWishList(string wishListName, string description = "")
        {
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(new WishList { Name = wishListName, Description = description }));
            try
            {
                var result = await this.PostAsyncNoCache<WishList>("/api/v1/wishlists", stringContent);
                if (result != null)
                {
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

        public async Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(string wishListName, string description = "")
        {
            var wishList = new WishList { Name = wishListName, Description = description };
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(wishList));

            try
            {
                var result = await this.PostAsyncNoCacheWithErrorMessage<WishList>("/api/v1/wishlists", stringContent);
                if (result?.Model != null)
                {
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

        public async Task<WishList> UpdateWishList(WishList wishList)
        {
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(wishList));
            try
            {
                var result = await this.PatchAsyncNoCache<WishList>("/api/v1/wishlists/" + wishList.Id, stringContent);
                if (result != null)
                {
                    await this.ClearWishListRelatedCacheAsync(wishList.Id);
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

        public async Task<WishListLine> AddProductToWishList(Guid wishListId, AddCartLine product)
        {
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(product));
            try
            {
                var result = await this.PostAsyncNoCache<WishListLine>("/api/v1/wishlists/" + wishListId + "/wishlistlines", stringContent);
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

        public async Task<bool> AddWishListLinesToWishList(Guid wishListId, WishListAddToCartCollection wishListLines)
        {
            var stringContent = await Task.Run(() => ServiceBase.SerializeModel(wishListLines));
            try
            {
                var result = await this.PostAsyncNoCache<object>("/api/v1/wishlists/" + wishListId + "/wishlistlines/batch", stringContent);

                await this.ClearWishListRelatedCacheAsync(wishListId);
                await this.ClearGetWishListsCacheAsync();

                return true;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<bool> LeaveWishList(Guid wishListId)
        {
            try
            {
                var response = await this.DeleteAsync($"/api/v1/wishlists/{wishListId}/share/current");

                // A NotFound response is treated as successful because the user wanted to leave the list anyway.
                var result = response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound;

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

        private string GetWishListUrl(Guid wishListId)
        {
            return $"/api/v1/wishlists/{wishListId}?expand=hiddenproducts,getalllines";
        }

        private string GetWishListsUrl(int pageNumber, int pageSize, WishListSortOrder sortOrder, string searchText = null)
        {
            var url = "/api/v1/wishlists?expand=top3products" + $"&page={pageNumber}" + $"&pageSize={pageSize}"
                   + $"&sort={SortOrderAttribute.GetSortOrderValue(sortOrder)}";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                url += "&query=" + WebUtility.UrlEncode(searchText);
            }

            return url;
        }
    }
}