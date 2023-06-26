using Akavache;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceApiSDK.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommerceSdk(
            this IServiceCollection services,
            string host,
            string clientId,
            string clientSecret,
            bool isCachingEnabled
        )
        {
            BlobCache.ApplicationName = "CommerceApiSDK";

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminAuthenticationService, AdminAuthenticationService>();
            services.AddScoped<IAdminClientService, AdminClientService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAutocompleteService, AutocompleteService>();
            services.AddScoped<IBillToService, BillToService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IFilesystemProvider, SimpleFilesystemProvider>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICatalogpagesService, CatalogpagesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IDashboardPanelsService, DashboardPanelsService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IJobQuoteService, JobQuoteService>();
            services.AddScoped<ILoggerService, DefaultLogger>();
            services.AddScoped<IMessengerService, MessengerService>();
            services.AddScoped<IMobileContentService, MobileContentService>();
            services.AddScoped<IMobileSpireContentService, MobileSpireContentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductV2Service, ProductV2Service>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<ITokenExConfigService, TokenExConfigService>();
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<IVmiLocationsService, VmiLocationsService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IWebsiteService, WebsiteService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddScoped<IRealTimePricingService, RealTimePricingService>();
            services.AddScoped<IRealTimeInventoryService, RealTimeInventoryService>();

            //ILocalStorageService needs to be implemented outside of the API SDK
            //INetworkService needs to be implemented outside of the API SDK
            //ISecureStorageService needs to be implemented outside of the API SDK
            //ITrackingService needs to be implemented outside of the API SDK

            ClientConfig.InitClientConfig(host, clientId, clientSecret, isCachingEnabled);

            return services;
        }
    }
}
