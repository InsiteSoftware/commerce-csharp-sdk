using Akavache;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Splat;

namespace CommerceApiSDK.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommerceSdk(this IServiceCollection services, string host, string clientId, string clientSecret, bool isCachingEnabled)
        {
            BlobCache.ApplicationName = "CommerceApiSDK";

            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IAdminAuthenticationService, AdminAuthenticationService>();
            services.AddSingleton<IAdminClientService, AdminClientService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAutocompleteService, AutocompleteService>();
            services.AddSingleton<IBillToService, BillToService>();
            services.AddSingleton<IBrandService, BrandService>();
            services.AddSingleton(Locator.Current.GetService<IFilesystemProvider>());
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<ICatalogpagesService, CatalogpagesService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IDashboardPanelsService, DashboardPanelsService>();
            services.AddSingleton<IDealerService, DealerService>();
            services.AddSingleton<IInvoiceService, InvoiceService>();
            services.AddSingleton<IJobQuoteService, JobQuoteService>();
            services.AddSingleton<ILoggerService, DefaultLogger>();
            services.AddSingleton<IMessengerService, MessengerService>();
            services.AddSingleton<IMobileContentService, MobileContentService>();
            services.AddSingleton<IMobileSpireContentService, MobileSpireContentService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductV2Service, ProductV2Service>();
            services.AddSingleton<IQuoteService, QuoteService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<ITokenExConfigService, TokenExConfigService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddSingleton<IVmiLocationsService, VmiLocationsService>();
            services.AddSingleton<IWarehouseService, WarehouseService>();
            services.AddSingleton<IWebsiteService, WebsiteService>();
            services.AddSingleton<IWishListService, WishListService>();
            services.AddSingleton<IRealTimePricingService, RealTimePricingService>();
            services.AddSingleton<IRealTimeInventoryService, RealTimeInventoryService>();
            services.AddSingleton<ICommerceAPIServiceProvider, CommerceAPIServiceProvider>();
            

            //ILocalStorageService needs to be implemented outside of the API SDK
            //INetworkService needs to be implemented outside of the API SDK
            //ISecureStorageService needs to be implemented outside of the API SDK
            //ITrackingService needs to be implemented outside of the API SDK

            ClientConfig.InitClientConfig(host, clientId, clientSecret, isCachingEnabled);

            return services;
        }
    }
}
