using System;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommerceApiSDK.Models
{
    public class Settings : BaseModel
    {
        public SettingsCollection SettingsCollection { get; set; }
    }

    public class AccountSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether creating an account is allowed.</summary>
        public bool AllowCreateAccount { get; set; }

        /// <summary>Gets or sets a value indicating whether guest checkout is allowed.</summary>
        public bool AllowGuestCheckout { get; set; }

        /// <summary>Gets or sets a value indicating whether subscribe to news letter is allowed.</summary>
        public bool AllowSubscribeToNewsLetter { get; set; }

        /// <summary>Gets or sets a value indicating whether select customer is required on sign in.</summary>
        public bool RequireSelectCustomerOnSignIn { get; set; }

        /// <summary>Gets or sets the minimum length of the password.</summary>
        public int PasswordMinimumLength { get; set; }

        /// <summary>Gets or sets the minimum required length for a password.</summary>
        public int PasswordMinimumRequiredLength { get; set; }

        /// <summary>Gets or sets a value indicating whether a special character is required in a password.</summary>
        public bool PasswordRequiresSpecialCharacter { get; set; }

        /// <summary>Gets or sets a value indicating whether an uppercase letter is required in a password.</summary>
        public bool PasswordRequiresUppercase { get; set; }

        /// <summary>Gets or sets a value indicating whether a lowercase letter is required in a password.</summary>
        public bool PasswordRequiresLowercase { get; set; }

        public bool RememberMe { get; set; }

        /// <summary>Gets or sets a value indicating whether a digit is required in a password.</summary>
        public bool PasswordRequiresDigit { get; set; }

        /// <summary>Gets or sets the days to retain user.</summary>
        public int DaysToRetainUser { get; set; }

        /// <summary>Gets or sets a value indicating whether email uses as user name.</summary>
        public bool UseEmailAsUserName { get; set; }

        public bool EnableWarehousePickup { get; set; }

        public bool LogOutUserAfterPasswordChange { get; set; }
    }

    public class CartSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether [show request delivery date].</summary>
        public bool CanRequestDeliveryDate { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can requisition.</summary>
        public bool CanRequisition { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can edit cost code.</summary>
        public bool CanEditCostCode { get; set; }

        /// <summary>Gets or sets a value of maximum delivery period.</summary>
        public int MaximumDeliveryPeriod { get; set; }

        /// <summary>Gets or sets a value indicating whether [show cost code].</summary>
        public bool ShowCostCode { get; set; }

        /// <summary>Gets or sets a value indicating whether [show po number].</summary>
        public bool ShowPoNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether [show pay pal].</summary>
        public bool ShowPayPal { get; set; }

        /// <summary>Gets or sets a value indicating whether [show credit card].</summary>
        public bool ShowCreditCard { get; set; }

        /// <summary>Gets or sets a value indicating whether [show tax and shipping].</summary>
        public bool ShowTaxAndShipping { get; set; }

        /// <summary>Gets or sets a value indicating whether [show line notes].</summary>
        public bool ShowLineNotes { get; set; }

        /// <summary>Gets or sets a value indicating whether [show subscription in footer].</summary>
        public bool ShowNewsletterSignup { get; set; }

        /// <summary>Gets or sets a value indicating whether [requires po number].</summary>
        public bool RequiresPoNumber { get; set; }

        /// <summary>Gets or sets a value indicating how long to display the add to cart confirmation popup.</summary>
        public int AddToCartPopupTimeout { get; set; }

        public bool EnableRequestPickUpDate { get; set; }

        public bool EnableSavedCreditCards { get; set; }

        public bool BypassCvvForSavedCards { get; set; }
    }

    public class ProductSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether products with no inventory can be ordered.</summary>
        [Obsolete("Use AllowBackOrderForDelivery instead.")]
        public bool AllowBackOrder { get; set; } = true;

        public bool AllowBackOrderForDelivery { get; set; }

        public bool AllowBackOrderForPickup { get; set; }

        /// <summary>Gets or sets a value indicating whether product inventory status is shown.</summary>
        public bool ShowInventoryAvailability { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether the add to cart confirmation dialog is shown. </summary>
        public bool ShowAddToCartConfirmationDialog { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether product comparison functionality is enabled.</summary>
        public bool EnableProductComparisons { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether alternate units of measure are displayed on the website.</summary>
        public bool AlternateUnitsOfMeasure { get; set; } = false;

        /// <summary>Gets or sets the third party review provider key.</summary>
        public string ThirdPartyReviews { get; set; } = "None";

        /// <summary>Gets or sets the default type of the view on the product list page.</summary>
        public string DefaultViewType { get; set; } = "List";

        /// <summary>Gets or sets a value indicating whether pricing for discounted products will include the amount saved in the selected currency.</summary>
        public bool ShowSavingsAmount { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether pricing for discounted products will include the percentage off the base price.</summary>
        public bool ShowSavingsPercent { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether pricing is obtained realtime from an external source.</summary>
        public bool RealTimePricing { get; set; } = false;

        /// <summary>Gets or sets a value indicating whether inventory is obtained realtime from an external source.</summary>
        public bool RealTimeInventory { get; set; } = false;

        /// <summary>Gets or sets a value indicating whether inventory is obtained realtime from an external source with pricing.</summary>
        public bool InventoryIncludedWithPricing { get; set; } = false;

        /// <summary>Gets or sets a value indicating what guests will be able to access before being prompted to sign in.</summary>
        public string StorefrontAccess { get; set; } = StorefrontAccessConstants.NoSignInRequired;

        public bool CanShowPriceFilters { get; set; }

        public bool CanSeeProducts { get; set; }

        public bool CanSeePrices { get; set; } = true;

        public bool CanAddToCart { get; set; }

        public string PricingService { get; set; }

        public bool DisplayAttributesInTabs { get; set; }

        public string AttributesTabSortOrder { get; set; }

        public bool DisplayDocumentsInTabs { get; set; }

        public string DocumentsTabSortOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether guests can see Inventory on a by warehouse basis. Defaults to false.</summary>
        public bool DisplayInventoryPerWarehouse { get; set; } = false;

        /// <summary>Gets or sets a value indicating whether Inventory by Warehouse only displays on the Product Detail page. Defaults to false</summary>
        public bool DisplayInventoryPerWarehouseOnlyOnProductDetail { get; set; } = false;

        public bool DisplayFacetsForStockeditems { get; set; }

        public string ImageProvider { get; set; }

        public string CatalogUrlPath { get; set; }

        public bool EnableVat { get; set; }

        public string VatPriceDisplay { get; set; }
    }

    public static class StorefrontAccessConstants
    {
        public static string NoSignInRequired = "NoSignInRequired";

        public static string SignInRequiredToBrowse = "SignInRequiredToBrowse";

        public static string SignInRequiredToAddToCart = "SignInRequiredToAddToCart";

        public static string SignInRequiredToAddToCartOrSeePrices =
            "SignInRequiredToAddToCartOrSeePrices";
    }

    public class SearchSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether autocomplete enabled.</summary>
        public bool AutocompleteEnabled { get; set; }

        /// <summary>Gets or sets a value indicating whether search history enabled.</summary>
        public bool SearchHistoryEnabled { get; set; }

        /// <summary>Gets or sets the search history limit.</summary>
        public int SearchHistoryLimit { get; set; }

        public bool EnableBoostingByPurchaseHistory { get; set; }

        public bool AllowFilteringForPreviouslyPurchasedProducts { get; set; }

        public string SearchPath { get; set; }
    }

    public class CustomerSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether allow bill to address edit.</summary>
        public bool AllowBillToAddressEdit { get; set; }

        /// <summary>Gets or sets a value indicating whether allow ship to address edit.</summary>
        public bool AllowShipToAddressEdit { get; set; }

        /// <summary>Gets or sets a value indicating whether allow create new ship to address.</summary>
        public bool AllowCreateNewShipToAddress { get; set; }

        /// <summary>Gets or sets a value indicating whether bill to company required.</summary>
        public bool BillToCompanyRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether bill to first name required.</summary>
        public bool BillToFirstNameRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether bill to last name required.</summary>
        public bool BillToLastNameRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether ship to company required.</summary>
        public bool ShipToCompanyRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether ship to first name required.</summary>
        public bool ShipToFirstNameRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether ship to last name required.</summary>
        public bool ShipToLastNameRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether budgets from online only.</summary>
        public bool BudgetsFromOnlineOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether bill to state required.</summary>
        public bool BillToStateRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether ship to state required.</summary>
        public bool ShipToStateRequired { get; set; }

        public bool DisplayAccountsReceivableBalances { get; set; }

        /// <summary>If Yes, an address may be entered on the address page that is only used for the current order. Default value: No.</summary>
        public bool AllowOneTimeAddresses { get; set; }
    }

    public class InvoiceSettings
    {
        /// <summary>Gets or sets the look back days.</summary>
        public int LookBackDays { get; set; }

        /// <summary>Gets or sets a value indicating whether show invoices.</summary>
        public bool ShowInvoices { get; set; }
    }

    public class OrderSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether allow cancellation request.</summary>
        [Obsolete("Use order status mapping")]
        public bool AllowCancellationRequest { get; set; }

        /// <summary>Gets or sets a value indicating whether quick order is allowed.</summary>
        public bool AllowQuickOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether items can be reordered</summary>
        public bool CanReorderItems { get; set; }

        /// <summary>Gets or sets a value indicating whether can order upload.</summary>
        public bool CanOrderUpload { get; set; }

        /// <summary>Gets or sets a value indicating whether RMAs are allowed.</summary>
        [Obsolete("Use order status mapping")]
        public bool AllowRma { get; set; }

        /// <summary>Gets or sets a value indicating whether show cost code.</summary>
        public bool ShowCostCode { get; set; }

        /// <summary>Gets or sets a value indicating whether show the PO number.</summary>
        public bool ShowPoNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether show the terms code.</summary>
        public bool ShowTermsCode { get; set; }

        /// <summary>Gets or sets a value indicating whether show the ERP order number.</summary>
        [Obsolete("Use ShowWebOrderNumber instead.")]
        public bool ShowErpOrderNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether show the web order number.</summary>
        public bool ShowWebOrderNumber { get; set; }

        public bool ShowOrderStatus { get; set; }

        /// <summary>Gets or sets a value indicating whether show orders.</summary>
        public bool ShowOrders { get; set; }

        /// <summary>Gets or sets the look back days.</summary>
        public int LookBackDays { get; set; }

        public bool VmiEnabled { get; set; }
    }

    public class QuoteSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether job quote enabled.</summary>
        public bool JobQuoteEnabled { get; set; }

        /// <summary>Gets or sets the quote expire days.</summary>
        public int QuoteExpireDays { get; set; }
    }

    public class WishListSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether allow multiple wish lists.</summary>
        public bool AllowMultipleWishLists { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether allow editing of wish lists.</summary>
        public bool AllowEditingOfWishLists { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether allow wish lists by customer.</summary>
        public bool AllowWishListsByCustomer { get; set; } = false;

        public bool AllowListSharing { get; set; } = true;

        public int ProductsPerPage { get; set; }

        public bool EnableWishListReminders { get; set; }
    }

    public class WebsiteSettings : BaseModel
    {
        /// <summary>Gets or sets a value indicating whether the mobile app is enabled for this website.</summary>
        public bool MobileAppEnabled { get; set; }

        public bool UseTokenExGateway { get; set; }

        public bool UseECheckTokenExGateway { get; set; }

        public bool TokenExTestMode { get; set; }

        public bool UsePaymetricGateway { get; set; }

        public bool UseAdyenDropIn { get; set; }

        public bool PaymentGatewayRequiresAuthentication { get; set; }

        public int DefaultPageSize { get; set; }

        public bool EnableCookiePrivacyPolicyPopup { get; set; }

        public bool EnableDynamicRecommendations { get; set; }

        public string GoogleMapsApiKey { get; set; }

        public string GoogleTrackingTypeComputed { get; set; }

        public string GoogleTrackingAccountId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CmsType? CmsType { get; set; }

        public bool IncludeSiteNameInPageTitle { get; set; }

        public string PageTitleDelimiter { get; set; }

        public bool SiteNameAfterTitle { get; set; }

        public string ReCaptchaSiteKey { get; set; }

        public bool ReCaptchaEnabledForContactUs { get; set; }

        public bool ReCaptchaEnabledForCreateAccount { get; set; }

        public bool ReCaptchaEnabledForForgotPassword { get; set; }

        public bool ReCaptchaEnabledForShareProduct { get; set; }

        public bool AdvancedSpireCmsFeatures { get; set; }

        public bool PreviewLoginEnabled { get; set; }

        public bool MaintenanceModeEnabled { get; set; }

        public bool UseSquareGateway { get; set; }

        public string SquareApplicationId { get; set; }

        public string SquareLocationId { get; set; }

        public bool SquareLive { get; set; }
    }

    public class TokenExDto
    {
        public string TokenExId { get; set; }

        public string Origin { get; set; }

        public string Timestamp { get; set; }

        public string TokenScheme { get; set; }

        public string AuthenticationKey { get; set; }

        public string Token { get; set; }
    }

    public class TokenExStyleDto
    {
        public string BaseColor { get; set; }

        public string FocusColor { get; set; }

        public string ErrorColor { get; set; }

        public string TextColor { get; set; }

        public string BorderWidth { get; set; }
    }

    public class PickUpSettings : BaseModel
    {
        /// <summary>Gets or sets the number of warehouses shown per page</summary>
        public int WarehousesPageSize { get; set; }

        /// <summary>Gets or sets the search radius for pick up .</summary>
        public int SearchRadius { get; set; }
    }

    public class MobileAppSettings : BaseModel
    {
        public string StartingCategoryForBrowsing { get; set; }

        public bool HasCheckout { get; set; }

        public bool OverrideCheckoutNavigation { get; set; }

        public string CheckoutUrl { get; set; }
    }

    public class SettingsCollection
    {
        public AccountSettings AccountSettings { get; set; }

        public CartSettings CartSettings { get; set; }

        public ProductSettings ProductSettings { get; set; }

        public SearchSettings SearchSettings { get; set; }

        public CustomerSettings CustomerSettings { get; set; }

        public InvoiceSettings InvoiceSettings { get; set; }

        public OrderSettings OrderSettings { get; set; }

        public QuoteSettings QuoteSettings { get; set; }

        public WishListSettings WishListSettings { get; set; }

        public WebsiteSettings WebsiteSettings { get; set; }

        public PickUpSettings PickUpSettings { get; set; }
    }
}
