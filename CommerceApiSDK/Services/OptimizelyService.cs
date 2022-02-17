using CommerceApiSDK.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace CommerceApiSDK.Services
{
    public class OptimizelyService : IOptimizelyService
    {
        public void Init(string host, string clientId, string clientSecret)
        {
            Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddSingleton<IAdminClientService, AdminClientService>();
                services.AddSingleton<ICacheService, CacheService>();
                services.AddSingleton<IClientService, IscClientService>();
                //Mvx.IoCProvider.RegisterType(() => Locator.Current.GetService<IFilesystemProvider>());


                services.AddSingleton<ISessionService, SessionService>();
                services.AddSingleton<ICategoryService, CategoryService>();
                services.AddSingleton<IAccountService, AccountService>();

                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IAdminAuthenticationService, AdminAuthenticationService>();



                services.AddSingleton<ISettingsService, SettingsService>();
                services.AddSingleton<IProductService, ProductService>();
                services.AddSingleton<IWebsiteService, WebsiteService>();
                services.AddSingleton<IWarehouseService, WarehouseService>();

                // services.AddSingleton<IAppConfigurationService, AppConfigurationService>();
                // services.AddSingleton<ILocatorService, LocatorService>();

                services.AddSingleton<IAddressService, AddressService>();
                services.AddSingleton<ICartLineService, CartLineService>();
                services.AddSingleton<ICartService, CartService>();
                services.AddSingleton<IOrderService, OrderService>();
                services.AddSingleton<IWishListService, WishListService>();
                services.AddSingleton<IWishListLineService, WishListLineService>();
                services.AddSingleton<IMobileContentService, MobileContentService>();
                services.AddSingleton<IMobileSpireContentService, MobileSpireContentService>();

                //services.AddSingleton<IContentConfigurationService, ContentConfigurationService>();

                services.AddSingleton<IAutocompleteService, AutocompleteService>();
                services.AddSingleton<IBrandService, BrandService>();
                services.AddSingleton<ITranslationService, TranslationService>();
                services.AddSingleton<IDealerService, DealerService>();
                services.AddSingleton<IPaymentProfileService, PaymentProfileService>();

            });
           
        }
    }
}
