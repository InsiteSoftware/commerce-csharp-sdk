namespace CommerceApiSDK
{
    public static class CommerceAPIConstants
    {
        public const string ShortDescriptionUnique = nameof(ShortDescriptionUnique);

        public const string ManufacturerItemUnique = nameof(ManufacturerItemUnique);

        public const string ManufacturerItem = nameof(ManufacturerItem);

        public const string CustomerNameUnique = nameof(CustomerNameUnique);

        public const string OfflineCacheDatabaseName = "blobs.db";

        public const string LocalStorageDatabaseName = "storage.db";

        public const int AddingToCartMillisecondsDelay = 5000;

        public const string CustomerName = nameof(CustomerName);

        public const string KeyWord = nameof(KeyWord);

        public const string ErpNumber = nameof(ErpNumber);

        public const string OrdersUrl = "/api/v1/orders";

        public const string OrdersShareUrl = "/api/v1/orders/shareorder";

        public const string OrderStatusMappingsUrl = "/api/v1/orderstatusmappings";

        public const string AccountUrl = "/api/v1/accounts";

        public const string CurrentPaymentProfiles = "/current/paymentprofiles";

        public const string PaymentProfileUrl = "/api/v1/accounts/current/paymentprofiles";

        public const string ResetPasswordUrl = "/admin/account/ForgotPassword";

        public const string AdminUserProfileUrl =
            "/api/v1/admin/AdminUserProfiles/Default.Default()";

        public const string AutocompleteUrl = "/api/v1/autocomplete";

        public const string BillTosUrl = "/api/v1/billtos";

        public const string BillToCurrentUrl = "/api/v1/billtos/current";

        public const string BillToCurrentShipTosUrl = "/api/v1/billtos/current/shiptos";

        public const string BillToCurrentShipToCurrentUrl =
            "/api/v1/billtos/current/shiptos/current";

        public const string BrandAlphabetUrl = "/api/v1/brandalphabet";

        public const string BrandUrl = "/api/v1/brands";

        public const string BrandCategoriesUrlFormat = "/api/v1/brands/{0}/categories";

        public const string BrandSubCategoriesUrlFormat = "/api/v1/brands/{0}/categories/{1}";

        public const string BrandProductLinesUrlFormat = "/api/v1/brands/{0}/productlines";

        public const string CartsUrl = "/api/v1/carts";

        public const string CartCurrentUrl = "/api/v1/carts/current";

        public const string CartCurrentCartLinesUrl = "/api/v1/carts/current/cartlines";

        public const string CartCurrentCartLineUrl = "api/v1/carts/current/cartlines";

        public const string CartCurrentPromotionsUrl = "/api/v1/carts/current/promotions";

        public const string CartPromotionsUrl = "/api/v1/carts/{0}/promotions";

        public const string CatalogPageUrl = "/api/v1/catalogpages?path=";

        public const string CategoryUrl = "/api/v1/categories";

        public const string MobileImageProperty = "mobileImage";

        public const string MobilePrimaryTextProperty = "mobilePrimaryText";

        public const string MobileSecondaryTextProperty = "mobileSecondaryText";

        public const string DashboardPanelUrl = "/api/v1/dashboardpanels";

        public const string DealersUrl = "/api/v1/dealers";

        public const string InvoicesUrl = "/api/v1/invoices";

        public const string JobQuoteUrl = "/api/v1/jobquotes";

        public const string MessageUrl = "/api/v1/messages";

        public const string MobileContentUrlFormat = "/api/v1/mobilecontent/{0}";

        public const string ContentUrl = "/api/v2/content/pageByType?type=Mobile/";

        public const string ProductsUrl = "/api/v1/products";

        public const string ProductsV2Url = "/api/v2/products";

        public const string RealTimePricingUrl = "/api/v1/realtimepricing";

        public const string RealTimeInventoryUrl = "/api/v1/realtimeinventory";

        public const string QuoteLineUrl = "/api/v1/quotes/{0}/quotelines/{1}";

        public const string QuoteUrl = "/api/v1/quotes";

        public const string PostSessionUrl = "/api/v1/sessions";

        public const string CurrentSessionUrl = "/api/v1/sessions/current";

        public const string SettingsUrl = "/api/v1/settings";

        public const string ProductSettingsUrl = "/api/v1/settings/products";

        public const string AccountSettingsUrl = "/api/v1/settings/account";

        public const string WebsiteSettingsUrl = "/api/v1/settings/website";

        public const string WishListSettingsUrl = "/api/v1/settings/wishlist";

        public const string CartSettingsUrl = "/api/v1/settings/cart";

        public const string MobileAppSettingsUrl = "/api/v1/settings/mobileapp";

        public const string QuoteSettingsUrl = "/api/v1/settings/quote";

        public const string TokenExConfigUrl = "/api/v1/tokenexconfig";

        public const string TranslationUrl = "/api/v1/translationdictionaries";

        public const string VmiLocationsUrl = "/api/v1/vmilocations";

        public const string WarehousesUrl = "/api/v1/warehouses";

        public const string WebsitesUrl = "/api/v1/websites/current";

        public const string WebsitesAddressFieldsUrl = "/api/v1/websites/current/addressfields";

        public const string WebsitesCountriesUrl = "/api/v1/websites/current/countries";

        public const string WebsitesCrossSellsUrl = "/api/v1/websites/current/crosssells";

        public const string WebsitesCurrenciesUrl = "/api/v1/websites/current/currencies";

        public const string WebsitesLanguagesUrl = "/api/v1/websites/current/languages";

        public const string WebsitesSiteMessagesUrl = "/api/v1/websites/current/sitemessages";

        public const string WebsitesStatesUrl = "/api/v1/websites/current/states";

        public const string TokenLogoutUrl = "identity/connect/endsession";

        public const string TokenValidationUrl = "identity/connect/accesstokenvalidation?token=";

        public const string TokenUrl = "identity/connect/token";

        public const string WishListUrl = "/api/v1/wishlists/";
    }
}
