using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class SettingsService : ServiceBase, ISettingsService
    {
        public SettingsService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<Settings>> GetSettingsAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<Settings>(
                    CommerceAPIConstants.SettingsUrl,
                    DefaultRequestTimeout
                );
                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load settings for {this.ClientService.Host}, but the settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Settings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<ProductSettings>> GetProductSettingsAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<ProductSettings>(
                    CommerceAPIConstants.ProductSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load product settings for {this.ClientService.Host}, but the product settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<ProductSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<AccountSettings>> GetAccountSettingsAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<AccountSettings>(
                    CommerceAPIConstants.AccountSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load account settings for {this.ClientService.Host}, but the account settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<AccountSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<WebsiteSettings>> GetWebsiteSettingsAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<WebsiteSettings>(
                    CommerceAPIConstants.WebsiteSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load website settings for {this.ClientService.Host}, but the website settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<WebsiteSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<WishListSettings>> GetWishListSettingAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<WishListSettings>(
                    CommerceAPIConstants.WishListSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load wish list settings for {this.ClientService.Host}, but the wish list settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<WishListSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<CartSettings>> GetCartSettingAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<CartSettings>(
                    CommerceAPIConstants.CartSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load cart settings for {this.ClientService.Host}, but the cart settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<CartSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<MobileAppSettings>> GetMobileAppSettingAsync()
        {
            try
            {
                var settings = await GetAsyncWithCachedResponse<MobileAppSettings>(
                    CommerceAPIConstants.MobileAppSettingsUrl,
                    DefaultRequestTimeout
                );

                if (settings.Model == null)
                {
                    throw new NullReferenceException(
                        $"Attempted to load mobile app settings for {this.ClientService.Host}, but the mobile app settings response is null. This website might either be an older ISC website or not an ISC website."
                    );
                }

                return settings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<MobileAppSettings>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteSettings>> GetQuoteSettingAsync()
        {
            try
            {
                return await GetAsyncNoCache<QuoteSettings>(CommerceAPIConstants.QuoteSettingsUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteSettings>(exception: exception);
            }
        }
    }
}
