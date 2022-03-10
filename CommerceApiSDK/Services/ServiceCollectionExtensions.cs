using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace CommerceApiSDK.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddComerceSdk(this IServiceCollection services, string host, string clientId, string clientSecret, bool isCachingEnabled)
        {
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IAdminAuthenticationService, AdminAuthenticationService>();
            services.AddSingleton<IAdminClientService, AdminClientService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAutocompleteService, AutocompleteService>();
            services.AddSingleton<IBillToService, BillToService>();
            services.AddSingleton<IBrandService, BrandService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICartLineService, CartLineService>();
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
            services.AddSingleton<IPaymentProfileService, PaymentProfileService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IQuoteLineService, QuoteLineService>();
            services.AddSingleton<IQuoteService, QuoteService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<ITokenExConfigService, TokenExConfigService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddSingleton<IVmiLocationsService, VmiLocationsService>();
            services.AddSingleton<IWarehouseService, WarehouseService>();
            services.AddSingleton<IWebsiteService, WebsiteService>();
            services.AddSingleton<IWishListLineService, WishListLineService>();
            services.AddSingleton<IWishListService, WishListService>();

            //ICacheService needs to be implemented outside of the API SDK
            //ILocalStorageService needs to be implemented outside of the API SDK
            //IMessageService needs to be implemented outside of the API SDK
            //INetworkService needs to be implemented outside of the API SDK
            //ISecureStorage needs to be implemented outside of the API SDK
            //ITrackingService needs to be implemented outside of the API SDK

            ClientConfig.InitClientConfig(host, clientId, clientSecret, isCachingEnabled);

            return services;
        }
    }
}
