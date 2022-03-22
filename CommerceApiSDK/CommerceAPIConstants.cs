namespace CommerceApiSDK
{
    public static class CommerceAPIConstants
    {
        public const string Shortdescriptionunique = "ShortDescriptionunique";

        public const string ManufacturerItemUnique = "ManufacturerItemunique";

        public const string Manufactureritem = "ManufacturerItem";

        public const string CustomerNameUnique = "CustomerNameunique";

        public const string OfflineCacheDatabaseName = "blobs.db";

        public const string LocalStorageDatabaseName = "storage.db";

        public const int AddingToCartMillisecondsDelay = 5000;

        public const string CustomerName = "CustomerName";

        public const string KeyWord = "word";

        public const string ErpNumber = "ERPNumber";

        public const string OrdersUrl = "/api/v1/orders";

        public const string OrderStatusMappingsUrl = "/api/v1/orderstatusmappings";

        public const string AccountUrl = "/api/v1/accounts";

        public const string CurrentPaymentProfiles = "/current/paymentprofiles";

        public const string PaymentProfileUri = "/api/v1/accounts/current/paymentprofiles";

        public const string CartUri = "/api/v1/carts/current";

        public const string ResetPasswordUri = "/admin/account/ForgotPassword";

        public const string AdminUserProfileUri = "/api/v1/admin/AdminUserProfiles/Default.Default()";

        public const string AutocompleteUrl = "/api/v1/autocomplete";

        public const string BillToToUrl = "/api/v1/billtos";

        public const string BrandAlphabetUrl = "/api/v1/brandalphabet";

        public const string BrandUrl = "/api/v1/brands";

        public const string BrandCategoriesUrlFormat = "/api/v1/brands/{0}/categories";

        public const string BrandSubCategoriesUrlFormat = "/api/v1/brands/{0}/categories/{1}";

        public const string BrandProductLinesUrlFormat = "/api/v1/brands/{0}/productlines";

        public const string CartLineUrl = "api/v1/carts/current/cartlines";

        public const string CartLinesUri = "/api/v1/carts/current/cartlines";

        public const string PromotionsUri = "/api/v1/carts/current/promotions";

        public const string CartsUri = "/api/v1/carts";

        public const string CatalogpageUrl = "/api/v1/catalogpages?path=";

        public const string CategoryUrl = "/api/v1/categories";

        public const string MobileImageProperty = "mobileImage";

        public const string MobilePrimaryTextProperty = "mobilePrimaryText";

        public const string MobileSecondaryTextProperty = "mobileSecondaryText";

        public const string DashboardPanelUrl = "/api/v1/dashboardpanels";

        public const string DealersUrl = "/api/v1/dealers";

        public const string InvoicesUrl = "/api/v1/invoices";

        public const string JobQuoteUrl = "/api/v1/jobquotes";

        public const string MessageUri = "/api/v1/messages";

        public const string mobileContentUrlFormat = "/api/v1/mobilecontent/{0}";

        public const string contentUrl = "/api/v2/content/pageByType?type=Mobile/";

        public const string ProductsUrl = "/api/v1/products";

        public const string RealTimePricingUrl = "/api/v1/realtimepricing";

        public const string RealTimeInventoryUrl = "/api/v1/realtimeinventory";

        public const string QuoteLineUri = "/api/v1/quotes/{0}/quotelines/{1}";

        public const string QuoteUri = "/api/v1/quotes";

        public const string PostSessionUri = "/api/v1/sessions";

        public const string CurrentSessionUri = "/api/v1/sessions/current";

        public const string SettingsUrl = "/api/v1/settings";

        public const string ProductSettingsUrl = "/api/v1/settings/products";

        public const string AccountSettingsUrl = "/api/v1/settings/account";

        public const string WebsiteSettingsUrl = "/api/v1/settings/website";

        public const string WishListSettingsUrl = "/api/v1/settings/wishlist";

        public const string CartSettingsUrl = "/api/v1/settings/cart";

        public const string MobileAppSettingsUrl = "/api/v1/settings/mobileapp";

        public const string QuoteSettingsUri = "/api/v1/settings/quote";

        public const string TokenexconfigUrl = "/api/v1/tokenexconfig";

        public const string TranslationUrl = "/api/v1/translationdictionaries";

        public const string VMILocationsUrl = "/api/v1/vmilocations";

        public const string WarehousesUrl = "/api/v1/warehouses";

        public const string WebsitesUrl = "/api/v1/websites/current";

        public const string WebsitesCrosssellsUrl = "/api/v1/websites/current/crosssells";

        public const string WebsitesSiteMessagesUrl = "/api/v1/websites/current/sitemessages";

        public const string WebsitesCountries = "/api/v1/websites/current/countries?expand=states";

        public const string WebsitesLanguagesUrl = "/api/v1/websites/current/languages";

        public const string TokenLogoutUri = "identity/connect/endsession";

        public const string TokenValidationUri = "identity/connect/accesstokenvalidation?token=";

        public const string TokenUri = "identity/connect/token";
    }
}