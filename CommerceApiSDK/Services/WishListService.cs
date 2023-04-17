using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class WishListService : ServiceBase, IWishListService
    {
        public WishListService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<WishListCollectionModel>> GetWishLists(WishListsQueryParameters parameters)
        {
            string url = CommerceAPIConstants.WishListUrl;

            url += parameters?.ToQueryString();

            try
            {
                return await GetAsyncWithCachedResponse<WishListCollectionModel>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishListCollectionModel>(exception: e);
            }
        }

        public async Task<ServiceResponse<WishList>> GetWishList(Guid wishListId, WishListQueryParameters parameters)
        {
            string url = GetWishListUrl(wishListId);

            url += parameters?.ToQueryString();

            try
            {
                return await GetAsyncWithCachedResponse<WishList>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishList>(exception: e);
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
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<ServiceResponse<WishList>> CreateWishList(CreateWishListQueryParameters parameters)
        {
            string url = CommerceAPIConstants.WishListUrl;

            StringContent stringContent = await Task.Run(
                () => SerializeModel(parameters.WishListObj)
            );
            try
            {
                var result = await PostAsyncNoCache<WishList>(url, stringContent);
                if (result != null)
                {
                    await ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishList>(exception: e);
            }
        }

        public async Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(
            CreateWishListQueryParameters parameters
        )
        {
            string url = CommerceAPIConstants.WishListUrl;

            StringContent stringContent = await Task.Run(
                () => SerializeModel(parameters.WishListObj)
            );

            try
            {
                ServiceResponse<WishList> result = await PostAsyncNoCacheWithErrorMessage<WishList>(
                    url,
                    stringContent
                );
                if (result?.Model != null)
                {
                    await ClearGetWishListsCacheAsync();
                }

                return result;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishList>(exception: e);
            }
        }

        public async Task<ServiceResponse<WishList>> UpdateWishList(WishList wishList)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishList));
            try
            {
                var response = await PatchAsyncNoCache<WishList>(
                    "/api/v1/wishlists/" + wishList.Id,
                    stringContent
                );
                WishList result = response.Model;
                if (result != null)
                {
                    await ClearWishListRelatedCacheAsync(wishList.Id);
                    await ClearGetWishListsCacheAsync();
                }

                return response;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishList>(exception: e);
            }
        }

        public async Task<ServiceResponse<WishListLine>> AddProductToWishList(Guid wishListId, AddCartLine product)
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(product));
            try
            {
                var response = await PostAsyncNoCache<WishListLine>(
                    "/api/v1/wishlists/" + wishListId + "/wishlistlines",
                    stringContent
                );

                WishListLine result = response.Model;
                if (result != null)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
                }

                return response;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishListLine>(exception: e);
            }
        }

        public async Task<bool> AddWishListLinesToWishList(
            Guid wishListId,
            WishListAddToCartCollection wishListLines
        )
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishListLines));
            try
            {
                object result = await PostAsyncNoCache<object>(
                    "/api/v1/wishlists/" + wishListId + "/wishlistlines/batch",
                    stringContent
                );

                await ClearWishListRelatedCacheAsync(wishListId);
                await ClearGetWishListsCacheAsync();

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
                HttpResponseMessage response = await DeleteAsync(
                    $"/api/v1/wishlists/{wishListId}/share/current"
                );

                // A NotFound response is treated as successful because the user wanted to leave the list anyway.
                bool result =
                    response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound;

                if (result)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
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

        public async Task<ServiceResponse<WishListLineCollectionModel>> GetWishListLines(
            Guid wishListId,
            WishListLineQueryParameters parameters
        )
        {
            try
            {
                string url = $"{CommerceAPIConstants.WishListUrl}/{wishListId}/wishlistlines";

                url += parameters?.ToQueryString();

                return await GetAsyncWithCachedResponse<WishListLineCollectionModel>(url);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishListLineCollectionModel>(exception: e);
            }
        }

        public async Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId)
        {
            try
            {
                HttpResponseMessage response = await DeleteAsync(
                    $"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLineId}"
                );
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
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<bool> DeleteWishListLineCollection(
            Guid wishListId,
            IList<WishListLine> wishListLineCollection
        )
        {
            if (wishListLineCollection == null || wishListLineCollection.Count <= 0)
            {
                return false;
            }

            string queryString =
                "?"
                + string.Join("&", wishListLineCollection.Select(o => $"wishListLineIds={o.Id}"));

            try
            {
                HttpResponseMessage response = await DeleteAsync(
                    $"/api/v1/wishlists/{wishListId}/wishlistlines/batch" + queryString
                );
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
                this.TrackingService.TrackException(e);
                return false;
            }
        }

        public async Task<ServiceResponse<WishListLine>> UpdateWishListLine(
            Guid wishListId,
            WishListLine wishListLine
        )
        {
            StringContent stringContent = await Task.Run(() => SerializeModel(wishListLine));
            try
            {
                var response = await PatchAsyncNoCache<WishListLine>(
                    $"/api/v1/wishlists/{wishListId}/wishlistlines/{wishListLine.Id}",
                    stringContent
                );
                WishListLine result = response.Model;

                if (result != null)
                {
                    await ClearWishListRelatedCacheAsync(wishListId);
                    await ClearGetWishListsCacheAsync();
                }

                return response;
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
                return GetServiceResponse<WishListLine>(exception: e);
            }
        }

        /// <summary>
        /// Clears the online (fast, in memory) cache of all endpoints that might be related with the specified wish list
        /// This method do not clear the offline cache by reason. The idea is that we want to be able to see something (even wrong) if we are offline or if we will became offline.
        /// On the other hand when the online cache is deleted if we have internet connection we will download the data again and we will overwrite hte offline cache.
        /// </summary>
        /// <param name="wishListId">The id of the wish list</param>
        /// <returns>void</returns>
        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task ClearWishListRelatedCacheAsync(Guid wishListId)
        {
            string prefix = this.ClientService.Host + $"/api/v1/wishlists/{wishListId}";
            await ClearOnlineCacheForUrlsStartingWith<WishListLineCollectionModel>(prefix);
            await ClearOnlineCacheForUrlsStartingWith<WishList>(prefix);
        }

        /// <summary>
        /// Clears the online (fast, in memory) cache of /getWishLists endpoint
        /// This method do not clear the offline cache by reason. The idea is that we want to be able to see something (even wrong) if we are offline or if we will became offline.
        /// On the other hand when the online cache is deleted if we have internet connection we will download the data again and we will overwrite hte offline cache.
        /// </summary>
        /// <returns>void</returns>
        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task ClearGetWishListsCacheAsync()
        {
            await ClearOnlineCacheForObjects<WishListCollectionModel>();
        }
    }
}
