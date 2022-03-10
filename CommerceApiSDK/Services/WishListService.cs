using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Attributes;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WishListService : WishListServiceBase, IWishListService
    {
        public WishListService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<WishListCollectionModel> GetWishLists(int pageNumber = 1, int pageSize = 16, WishListSortOrder sortOrder = WishListSortOrder.ModifiedOnDescending, string searchText = null)
        {
            string url = GetWishListsUrl(pageNumber, pageSize, sortOrder, searchText);

            try
            {
                return await GetAsyncWithCachedResponse<WishListCollectionModel>(url);
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<WishList> GetWishList(Guid wishListId)
        {
            string url = GetWishListUrl(wishListId);

            try
            {
                return await GetAsyncWithCachedResponse<WishList>(url);
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<bool> DeleteWishList(Guid wishListId)
        {
            try
            {
                HttpResponseMessage response = await DeleteAsync("/api/v1/wishlists/" + wishListId);
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

        public async Task<WishList> CreateWishList(string wishListName, string description = "")
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(new WishList { Name = wishListName, Description = description }));
            try
            {
                WishList result = await PostAsyncNoCache<WishList>("/api/v1/wishlists", stringContent);
                if (result != null)
                {
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

        public async Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(string wishListName, string description = "")
        {
            WishList wishList = new WishList() { Name = wishListName, Description = description };
            StringContent stringContent = await Task.Run(() => SerializeModel(wishList));

            try
            {
                ServiceResponse<WishList> result = await PostAsyncNoCacheWithErrorMessage<WishList>("/api/v1/wishlists", stringContent);
                if (result?.Model != null)
                {
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

        public async Task<WishList> UpdateWishList(WishList wishList)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishList));
            try
            {
                WishList result = await PatchAsyncNoCache<WishList>("/api/v1/wishlists/" + wishList.Id, stringContent);
                if (result != null)
                {
                    await ClearWishListRelatedCacheAsync(wishList.Id);
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

        public async Task<WishListLine> AddProductToWishList(Guid wishListId, AddCartLine product)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(product));
            try
            {
                WishListLine result = await PostAsyncNoCache<WishListLine>("/api/v1/wishlists/" + wishListId + "/wishlistlines", stringContent);
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

        public async Task<bool> AddWishListLinesToWishList(Guid wishListId, WishListAddToCartCollection wishListLines)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishListLines));
            try
            {
                object result = await PostAsyncNoCache<object>("/api/v1/wishlists/" + wishListId + "/wishlistlines/batch", stringContent);

                await ClearWishListRelatedCacheAsync(wishListId);
                await ClearGetWishListsCacheAsync();

                return true;
            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<bool> LeaveWishList(Guid wishListId)
        {
            try
            {
                HttpResponseMessage response = await DeleteAsync($"/api/v1/wishlists/{wishListId}/share/current");

                // A NotFound response is treated as successful because the user wanted to leave the list anyway.
                bool result = response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound;

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

        private string GetWishListUrl(Guid wishListId)
        {
            return $"/api/v1/wishlists/{wishListId}?expand=hiddenproducts,getalllines";
        }

        private string GetWishListsUrl(int pageNumber, int pageSize, WishListSortOrder sortOrder, string searchText = null)
        {
            string url = "/api/v1/wishlists?expand=top3products" + $"&page={pageNumber}" + $"&pageSize={pageSize}"
                   + $"&sort={SortOrderAttribute.GetSortOrderValue(sortOrder)}";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                url += "&query=" + WebUtility.UrlEncode(searchText);
            }

            return url;
        }
    }
}