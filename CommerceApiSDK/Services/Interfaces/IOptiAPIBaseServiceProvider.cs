using System;

namespace CommerceApiSDK.Services.Interfaces
{
     public interface ICommerceAPIServiceProvider
    {
        IAccountService GetAccountService();
        IAdminAuthenticationService GetAdminAuthenticationService();
        IAdminClientService GetAdminClientService();
        IAuthenticationService GetAuthenticationService();
        IAutocompleteService GetAutocompleteService();
        IBillToService GetBillToService();
        IBrandService GetBrandService();
        ICacheService GetCacheService();
        ICartService GetCartService();
        ICatalogpagesService GetCatalogpagesService();
        ICategoryService GetCategoryService();
        IClientService GetClientService();
        IDashboardPanelsService GetDashboardPanelsService();
        IDealerService GetDealerService();
        IInvoiceService GetInvoiceService();
        IJobQuoteService GetJobQuoteService();
        ILocalStorageService GetLocalStorageService();
        ILoggerService GetLoggerService();
        IMessageService GetMessageService();
        IMessengerService GetMessengerService();
        IMobileContentService GetMobileContentService();
        IMobileSpireContentService GetMobileSpireContentService();
        INetworkService GetNetworkService();
        IOptimizelyService GetOptimizelyService();
        IOrderService GetOrderService();
        IProductService GetProductService();
        IProductV2Service GetProductV2Service();
        IQuoteService GetQuoteService();
        ISecureStorageService GetSecureStorageService();
        ISessionService GetSessionService();
        ISettingsService GetSettingsService();
        ITokenExConfigService GetTokenExConfigService();
        ITrackingService GetTrackingService();
        ITranslationService GetTranslationService();
        IVmiLocationsService GetVmiLocationsService();
        IWarehouseService GetWarehouseService();
        IWebsiteService GetWebsiteService();
        IWishListService GetWishListService();
        IRealTimePricingService GetRealTimePricingService();
        IRealTimeInventoryService GetRealTimeInventoryService();
    }
}
