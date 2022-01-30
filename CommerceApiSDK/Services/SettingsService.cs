namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;

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
                var settings = await this.GetAsyncWithCachedResponse<Settings>(SettingsUrl, ServiceBase.DefaultRequestTimeout);
                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load settings for {this.Client.Host}, but the settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ProductSettings> GetProductSettingsAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<ProductSettings>(ProductSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load product settings for {this.Client.Host}, but the product settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AccountSettings> GetAccountSettingsAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<AccountSettings>(AccountSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load account settings for {this.Client.Host}, but the account settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<WebsiteSettings> GetWebsiteSettingsAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<WebsiteSettings>(WebsiteSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load website settings for {this.Client.Host}, but the website settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<WishListSettings> GetWishListSettingAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<WishListSettings>(WishListSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load wish list settings for {this.Client.Host}, but the wish list settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<CartSettings> GetCartSettingAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<CartSettings>(CartSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load cart settings for {this.Client.Host}, but the cart settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<MobileAppSettings> GetMobileAppSettingAsync()
        {
            try
            {
                var settings = await this.GetAsyncWithCachedResponse<MobileAppSettings>(MobileAppSettingsUrl, ServiceBase.DefaultRequestTimeout);

                if (settings == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load mobile app settings for {this.Client.Host}, but the mobile app settings response is null. This website might either be an older ISC website or not an ISC website.");
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteSetting> GetQuoteSettingAsync()
        {
            try
            {
                return await this.GetAsyncNoCache<QuoteSetting>(QuoteSettingsUri);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}