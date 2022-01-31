namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Services.Interfaces;

    public class WishListServiceBase : ServiceBase
    {
        public WishListServiceBase(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService) : base(clientService, networkService, trackingService, cacheService)
        {
        }

        /// <summary>
        /// Clears the online (fast, in memory) cache of all endpoints that might be related with the specified wish list
        /// This method do not clear the offline cache by reason. The idea is that we want to be able to see something (even wrong) if we are offline or if we will became offline.
        /// On the other hand when the online cache is deleted if we have internet connection we will download the data again and we will overwrite hte offline cache.
        /// </summary>
        /// <param name="wishListId">The id of the wish list</param>
        /// <returns>void</returns>
        protected async Task ClearWishListRelatedCacheAsync(Guid wishListId)
        {
            string prefix = Client.Host + $"/api/v1/wishlists/{wishListId}";
            await ClearOnlineCacheForUrlsStartingWith<WishListLineCollectionModel>(prefix);
            await ClearOnlineCacheForUrlsStartingWith<WishList>(prefix);
        }

        /// <summary>
        /// Clears the online (fast, in memory) cache of /getWishLists endpoint
        /// This method do not clear the offline cache by reason. The idea is that we want to be able to see something (even wrong) if we are offline or if we will became offline.
        /// On the other hand when the online cache is deleted if we have internet connection we will download the data again and we will overwrite hte offline cache.
        /// </summary>
        /// <returns>void</returns>
        protected async Task ClearGetWishListsCacheAsync()
        {
            await ClearOnlineCacheForObjects<WishListCollectionModel>();
        }
    }
}
