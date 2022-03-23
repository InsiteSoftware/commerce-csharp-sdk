using System;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class CommerceAPIServiceProvider : ICommerceAPIServiceProvider
    {
        private readonly IAccountService _accountService;
        private readonly IAdminAuthenticationService _adminAuthenticationService;
        private readonly IAdminClientService _adminClientService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAutocompleteService _autocompleteService;
        private readonly IBillToService _billToService;
        private readonly IBrandService _brandService;
        private readonly ICacheService _cacheService;
        private readonly ICartLineService _cartLineService;
        private readonly ICartService _cartService;
        private readonly ICatalogpagesService _catalogpagesService;
        private readonly ICategoryService _categoryService;
        private readonly IClientService _clientService;
        private readonly IDashboardPanelsService _dashboardPanelsService;
        private readonly IDealerService _dealerService;
        private readonly IInvoiceService _invoiceService;
        private readonly IJobQuoteService _jobQuoteService;
        private readonly ILocalStorageService _localStorageService;
        private readonly ILoggerService _loggerService;
        private readonly IMessageService _messageService;
        private readonly IMessengerService _messengerService;
        private readonly IMobileContentService _mobileContentService;
        private readonly IMobileSpireContentService _mobileSpireContentService;
        private readonly INetworkService _networkService;
        private readonly IOptimizelyService _optimizelyService;
        private readonly IOrderService _orderService;
        private readonly IPaymentProfileService _paymentProfileService;
        private readonly IProductService _productService;
        private readonly IProductV2Service _productV2Service;
        private readonly IQuoteLineService _quoteLineService;
        private readonly IQuoteService _quoteService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly ISessionService _sessionService;
        private readonly ISettingsService _settingsService;
        private readonly ITokenExConfigService _tokenExConfigService;
        private readonly ITrackingService _trackingService;
        private readonly ITranslationService _translationService;
        private readonly IVmiLocationsService _vmiLocationsService;
        private readonly IWarehouseService _warehouseService;
        private readonly IWebsiteService _websiteService;
        private readonly IWishListLineService _wishListLineService;
        private readonly IWishListService _wishListService;

        public CommerceAPIServiceProvider(IAccountService accountService, IAdminAuthenticationService adminAuthenticationService, IAdminClientService adminClientService, IAuthenticationService authenticationService,
            IAutocompleteService autocompleteService, IBillToService billToService, IBrandService brandService, ICacheService cacheService, ICartLineService cartLineService,
            ICartService cartService, ICatalogpagesService catalogpagesService, ICategoryService categoryService, IClientService clientService, IDashboardPanelsService dashboardPanelsService,
            IDealerService dealerService, IInvoiceService invoiceService, IJobQuoteService jobQuoteService, ILocalStorageService localStorageService,
            ILoggerService loggerService, IMessageService messageService, IMessengerService messengerService, IMobileContentService mobileContentService, IMobileSpireContentService mobileSpireContentService,
            INetworkService networkService, IOptimizelyService optimizelyService, IOrderService orderService, IPaymentProfileService paymentProfileService, IProductService productService,
            IProductV2Service productV2Service, IQuoteLineService quoteLineService, IQuoteService quoteService, ISecureStorageService secureStorageService, ISessionService sessionService,
            ISettingsService settingsService, ITokenExConfigService tokenExConfigService, ITrackingService trackingService, ITranslationService translationService, IVmiLocationsService vmiLocationsService,
            IWarehouseService warehouseService, IWebsiteService websiteService, IWishListLineService wishListLineService, IWishListService wishListService)
        {
            _accountService = accountService;
            _adminAuthenticationService = adminAuthenticationService;
            _adminClientService = adminClientService;
            _authenticationService = authenticationService;
            _autocompleteService = autocompleteService;
            _billToService = billToService;
            _brandService = brandService;
            _cacheService = cacheService;
            _cartLineService = cartLineService;
            _cartService = cartService;
            _catalogpagesService = catalogpagesService;
            _categoryService = categoryService;
            _clientService = clientService;
            _dashboardPanelsService = dashboardPanelsService;
            _dealerService = dealerService;
            _invoiceService = invoiceService;
            _jobQuoteService = jobQuoteService;
            _localStorageService = localStorageService;
            _loggerService = loggerService;
            _messageService = messageService;
            _messengerService = messengerService;
            _mobileContentService = mobileContentService;
            _mobileSpireContentService = mobileSpireContentService;
            _networkService = networkService;
            _optimizelyService = optimizelyService;
            _orderService = orderService;
            _paymentProfileService = paymentProfileService;
            _productService = productService;
            _productV2Service = productV2Service;
            _quoteLineService = quoteLineService;
            _quoteService = quoteService;
            _secureStorageService = secureStorageService;
            _sessionService = sessionService;
            _settingsService = settingsService;
            _tokenExConfigService = tokenExConfigService;
            _trackingService = trackingService;
            _translationService = translationService;
            _vmiLocationsService = vmiLocationsService;
            _warehouseService = warehouseService;
            _websiteService = websiteService;
            _wishListLineService = wishListLineService;
            _wishListService = wishListService;
        }

        public IAccountService GetAccountService()
        {
            return _accountService;
        }

        public IAdminAuthenticationService GetAdminAuthenticationService()
        {
            return _adminAuthenticationService;
        }

        public IAdminClientService GetAdminClientService()
        {
            return _adminClientService;
        }

        public IAuthenticationService GetAuthenticationService()
        {
            return _authenticationService;
        }

        public IAutocompleteService GetAutocompleteService()
        {
            return _autocompleteService;
        }

        public IBillToService GetBillToService()
        {
            return _billToService;
        }

        public IBrandService GetBrandService()
        {
            return _brandService;
        }

        public ICacheService GetCacheService()
        {
            return _cacheService;
        }

        public ICartLineService GetCartLineService()
        {
            return _cartLineService;
        }

        public ICartService GetCartService()
        {
            return _cartService;
        }

        public ICatalogpagesService GetCatalogpagesService()
        {
            return _catalogpagesService;
        }

        public ICategoryService GetCategoryService()
        {
            return _categoryService;
        }

        public IClientService GetClientService()
        {
            return _clientService;
        }

        public IDashboardPanelsService GetDashboardPanelsService()
        {
            return _dashboardPanelsService;
        }

        public IDealerService GetDealerService()
        {
            return _dealerService;
        }

        public IInvoiceService GetInvoiceService()
        {
            return _invoiceService;
        }

        public IJobQuoteService GetJobQuoteService()
        {
            return _jobQuoteService;
        }

        public ILocalStorageService GetLocalStorageService()
        {
            return _localStorageService;
        }

        public ILoggerService GetLoggerService()
        {
            return _loggerService;
        }

        public IMessageService GetMessageService()
        {
            return _messageService;
        }

        public IMessengerService GetMessengerService()
        {
            return _messengerService;
        }

        public IMobileContentService GetMobileContentService()
        {
            return _mobileContentService;
        }

        public IMobileSpireContentService GetMobileSpireContentService()
        {
            return _mobileSpireContentService;
        }

        public INetworkService GetNetworkService()
        {
            return _networkService;
        }

        public IOptimizelyService GetOptimizelyService()
        {
            return _optimizelyService;
        }

        public IOrderService GetOrderService()
        {
            return _orderService;
        }

        public IPaymentProfileService GetPaymentProfileService()
        {
            return _paymentProfileService;
        }

        public IProductService GetProductService()
        {
            return _productService;
        }

        public IProductV2Service GetProductV2Service()
        {
            return _productV2Service;
        }

        public IQuoteLineService GetQuoteLineService()
        {
            return _quoteLineService;
        }

        public IQuoteService GetQuoteService()
        {
            return _quoteService;
        }

        public ISecureStorageService GetSecureStorageService()
        {
            return _secureStorageService;
        }

        public ISessionService GetSessionService()
        {
            return _sessionService;
        }

        public ISettingsService GetSettingsService()
        {
            return _settingsService;
        }

        public ITokenExConfigService GetTokenExConfigService()
        {
            return _tokenExConfigService;
        }

        public ITrackingService GetTrackingService()
        {
            return _trackingService;
        }

        public ITranslationService GetTranslationService()
        {
            return _translationService;
        }

        public IVmiLocationsService GetVmiLocationsService()
        {
            return _vmiLocationsService;
        }

        public IWarehouseService GetWarehouseService()
        {
            return _warehouseService;
        }

        public IWebsiteService GetWebsiteService()
        {
            return _websiteService;
        }

        public IWishListLineService GetWishListLineService()
        {
            return _wishListLineService;
        }

        public IWishListService GetWishListService()
        {
            return _wishListService;
        }
    }
}