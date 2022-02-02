using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class SettingsService : ServiceBase, ISettingsService
    {
        private const string SettingsUrl = "/api/v1/settings";
        private const string ProductSettingsUrl = "/api/v1/settings/products";
        private const string AccountSettingsUrl = "/api/v1/settings/account";
        private const string WebsiteSettingsUrl = "/api/v1/settings/website";
        private const string WishListSettingsUrl = "/api/v1/settings/wishlist";
        private const string CartSettingsUrl = "/api/v1/settings/cart";
        private const string MobileAppSettingsUrl = "/api/v1/settings/mobileapp";
        private const string QuoteSettingsUri = "/api/v1/settings/quote";

        public SettingsService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<Settings> GetSettingsAsync()
        {
            try
            {
                Settings settings = await GetAsyncWithCachedResponse<Settings>(SettingsUrl, DefaultRequestTimeout);
                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load settings for {Client.Host}, but the settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductSettings> GetProductSettingsAsync()
        {
            try
            {
                ProductSettings settings = await GetAsyncWithCachedResponse<ProductSettings>(ProductSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load product settings for {Client.Host}, but the product settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AccountSettings> GetAccountSettingsAsync()
        {
            try
            {
                AccountSettings settings = await GetAsyncWithCachedResponse<AccountSettings>(AccountSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load account settings for {Client.Host}, but the account settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<WebsiteSettings> GetWebsiteSettingsAsync()
        {
            try
            {
                WebsiteSettings settings = await GetAsyncWithCachedResponse<WebsiteSettings>(WebsiteSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load website settings for {Client.Host}, but the website settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<WishListSettings> GetWishListSettingAsync()
        {
            try
            {
                WishListSettings settings = await GetAsyncWithCachedResponse<WishListSettings>(WishListSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load wish list settings for {Client.Host}, but the wish list settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<CartSettings> GetCartSettingAsync()
        {
            try
            {
                CartSettings settings = await GetAsyncWithCachedResponse<CartSettings>(CartSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load cart settings for {Client.Host}, but the cart settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<MobileAppSettings> GetMobileAppSettingAsync()
        {
            try
            {
                MobileAppSettings settings = await GetAsyncWithCachedResponse<MobileAppSettings>(MobileAppSettingsUrl, DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load mobile app settings for {Client.Host}, but the mobile app settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteSetting> GetQuoteSettingAsync()
        {
            try
            {
                return await GetAsyncNoCache<QuoteSetting>(QuoteSettingsUri);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}