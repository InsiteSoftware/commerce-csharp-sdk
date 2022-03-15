using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class SettingsService : ServiceBase, ISettingsService
    {
        public SettingsService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<Settings> GetSettingsAsync()
        {
            try
            {
                Settings settings = await GetAsyncWithCachedResponse<Settings>(CommerceAPIConstants.SettingsUrl, DefaultRequestTimeout);
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
                ProductSettings settings = await GetAsyncWithCachedResponse<ProductSettings>(CommerceAPIConstants.ProductSettingsUrl, DefaultRequestTimeout);

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
                AccountSettings settings = await GetAsyncWithCachedResponse<AccountSettings>(CommerceAPIConstants.AccountSettingsUrl, DefaultRequestTimeout);

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
                WebsiteSettings settings = await GetAsyncWithCachedResponse<WebsiteSettings>(CommerceAPIConstants.WebsiteSettingsUrl, DefaultRequestTimeout);

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
                WishListSettings settings = await GetAsyncWithCachedResponse<WishListSettings>(CommerceAPIConstants.WishListSettingsUrl, DefaultRequestTimeout);

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
                CartSettings settings = await GetAsyncWithCachedResponse<CartSettings>(CommerceAPIConstants.CartSettingsUrl, DefaultRequestTimeout);

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
                MobileAppSettings settings = await GetAsyncWithCachedResponse<MobileAppSettings>(CommerceAPIConstants.MobileAppSettingsUrl, DefaultRequestTimeout);

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
                return await GetAsyncNoCache<QuoteSetting>(CommerceAPIConstants.QuoteSettingsUri);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}