using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{


    public class CartCollection: BaseModel
    {
        public string uri { get; set; }
        public string cartLinesUri { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string statusDisplay { get; set; }
        public string type { get; set; }
        public string typeDisplay { get; set; }
        public string orderNumber { get; set; }
        public string erpOrderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public BillTo billTo { get; set; }
        public ShipTo shipTo { get; set; }
        public string userLabel { get; set; }
        public string userRoles { get; set; }
        public string shipToLabel { get; set; }
        public string notes { get; set; }
        public Carrier carrier { get; set; }
        public ShipVia shipVia { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public string fulfillmentMethod { get; set; }
        public string requestedPickupDate { get; set; }
        public string poNumber { get; set; }
        public string promotionCode { get; set; }
        public string initiatedByUserName { get; set; }
        public int totalQtyOrdered { get; set; }
        public int lineCount { get; set; }
        public int totalCountDisplay { get; set; }
        public int quoteRequiredCount { get; set; }
        public decimal orderSubTotal { get; set; }
        public string orderSubTotalDisplay { get; set; }
        public decimal orderSubTotalWithOutProductDiscounts { get; set; }
        public string orderSubTotalWithOutProductDiscountsDisplay { get; set; }
        public decimal totalTax { get; set; }
        public string totalTaxDisplay { get; set; }
        public decimal shippingAndHandling { get; set; }
        public string shippingAndHandlingDisplay { get; set; }
        public decimal orderGrandTotal { get; set; }
        public string orderGrandTotalDisplay { get; set; }
        public string costCodeLabel { get; set; }
        public bool isAuthenticated { get; set; }
        public bool isGuestOrder { get; set; }
        public bool isSalesperson { get; set; }
        public bool isSubscribed { get; set; }
        public bool requiresPoNumber { get; set; }
        public bool displayContinueShoppingLink { get; set; }
        public bool canModifyOrder { get; set; }
        public bool canSaveOrder { get; set; }
        public bool canBypassCheckoutAddress { get; set; }
        public bool canRequisition { get; set; }
        public bool canRequestQuote { get; set; }
        public bool canEditCostCode { get; set; }
        public bool showTaxAndShipping { get; set; }
        public bool showLineNotes { get; set; }
        public bool showCostCode { get; set; }
        public bool showNewsletterSignup { get; set; }
        public bool showPoNumber { get; set; }
        public bool showCreditCard { get; set; }
        public bool showECheck { get; set; }
        public bool showPayPal { get; set; }
        public bool isAwaitingApproval { get; set; }
        public bool requiresApproval { get; set; }
        public string approverReason { get; set; }
        public bool hasApprover { get; set; }
        public string salespersonName { get; set; }
        public PaymentOptions paymentOptions { get; set; }
        public IList<CostCode> costCodes { get; set; }
        public IList<Carrier> carriers { get; set; }
        public IList<Warehouse> warehouses { get; set; }
        public IList<CartLine> cartLines { get; set; }
        public IList<CustomerOrderTaxis> customerOrderTaxes { get; set; }
        public bool canCheckOut { get; set; }
        public bool hasInsufficientInventory { get; set; }
        public string currencySymbol { get; set; }
        public string requestedDeliveryDate { get; set; }
        public DateTime? requestedDeliveryDateDisplay { get; set; }
        public bool cartNotPriced { get; set; }
        public IList<string> messages { get; set; }
        public CreditCardBillingAddress creditCardBillingAddress { get; set; }
        public IList<AlsoPurchasedProduct> alsoPurchasedProducts { get; set; }
        public DateTime? requestedPickupDateDisplay { get; set; }
        public string taxFailureReason { get; set; }
        public bool failedToGetRealTimeInventory { get; set; }
        public bool unassignCart { get; set; }
        public string customerVatNumber { get; set; }
        public string vmiLocationId { get; set; }
        public Properties properties { get; set; }



        public class Accessory : BaseModel
        {
        }



        public class ActualBreakPrice : BaseModel
        {
            public int breakQty { get; set; }
            public int breakPrice { get; set; }
            public string breakPriceDisplay { get; set; }
            public string savingsMessage { get; set; }
            public int breakPriceWithVat { get; set; }
            public string breakPriceWithVatDisplay { get; set; }
        }

        public class AdditionalResults : BaseModel
        {
        }

        public class Address1 : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class Address2 : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class Address3 : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class Address4 : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class AggregateFieldScore : BaseModel
        {
            public string name { get; set; }
            public int score { get; set; }
        }



        public class AgingBucketFuture : BaseModel
        {
            public int amount { get; set; }
            public string amountDisplay { get; set; }
            public string label { get; set; }
        }

        public class AgingBucketTotal : BaseModel
        {
            public int amount { get; set; }
            public string amountDisplay { get; set; }
            public string label { get; set; }
        }

        public class AlsoPurchasedProduct : BaseModel
        {
            public string id { get; set; }
            public string orderLineId { get; set; }
            public string name { get; set; }
            public string customerName { get; set; }
            public string shortDescription { get; set; }
            public string erpNumber { get; set; }
            public string erpDescription { get; set; }
            public string urlSegment { get; set; }
            public int basicListPrice { get; set; }
            public int basicSalePrice { get; set; }
            public DateTime basicSaleStartDate { get; set; }
            public DateTime basicSaleEndDate { get; set; }
            public string smallImagePath { get; set; }
            public string mediumImagePath { get; set; }
            public string largeImagePath { get; set; }
            public Pricing pricing { get; set; }
            public string currencySymbol { get; set; }
            public int qtyOnHand { get; set; }
            public bool isConfigured { get; set; }
            public bool isFixedConfiguration { get; set; }
            public bool isActive { get; set; }
            public bool isHazardousGood { get; set; }
            public bool isDiscontinued { get; set; }
            public bool isSpecialOrder { get; set; }
            public bool isGiftCard { get; set; }
            public bool isBeingCompared { get; set; }
            public bool isSponsored { get; set; }
            public bool isSubscription { get; set; }
            public bool quoteRequired { get; set; }
            public string manufacturerItem { get; set; }
            public string packDescription { get; set; }
            public string altText { get; set; }
            public string customerUnitOfMeasure { get; set; }
            public bool canBackOrder { get; set; }
            public bool trackInventory { get; set; }
            public int multipleSaleQty { get; set; }
            public int minimumOrderQty { get; set; }
            public string htmlContent { get; set; }
            public string productCode { get; set; }
            public string priceCode { get; set; }
            public string sku { get; set; }
            public string upcCode { get; set; }
            public string modelNumber { get; set; }
            public string taxCode1 { get; set; }
            public string taxCode2 { get; set; }
            public string taxCategory { get; set; }
            public string shippingClassification { get; set; }
            public string shippingLength { get; set; }
            public string shippingWidth { get; set; }
            public string shippingHeight { get; set; }
            public string shippingWeight { get; set; }
            public int qtyPerShippingPackage { get; set; }
            public int shippingAmountOverride { get; set; }
            public int handlingAmountOverride { get; set; }
            public string metaDescription { get; set; }
            public string metaKeywords { get; set; }
            public string pageTitle { get; set; }
            public bool allowAnyGiftCardAmount { get; set; }
            public int sortOrder { get; set; }
            public bool hasMsds { get; set; }
            public string unspsc { get; set; }
            public string roundingRule { get; set; }
            public string vendorNumber { get; set; }
            public ConfigurationDto configurationDto { get; set; }
            public string unitOfMeasure { get; set; }
            public string unitOfMeasureDisplay { get; set; }
            public string unitOfMeasureDescription { get; set; }
            public string selectedUnitOfMeasure { get; set; }
            public string selectedUnitOfMeasureDisplay { get; set; }
            public string productDetailUrl { get; set; }
            public bool canAddToCart { get; set; }
            public bool allowedAddToCart { get; set; }
            public bool canAddToWishlist { get; set; }
            public bool canViewDetails { get; set; }
            public bool canShowPrice { get; set; }
            public bool canShowUnitOfMeasure { get; set; }
            public bool canEnterQuantity { get; set; }
            public bool canConfigure { get; set; }
            public bool isStyleProductParent { get; set; }
            public string styleParentId { get; set; }
            public bool requiresRealTimeInventory { get; set; }
            public int numberInCart { get; set; }
            public int qtyOrdered { get; set; }
            public Availability availability { get; set; }
            public IList<StyleTrait> styleTraits { get; set; }
            public IList<StyledProduct> styledProducts { get; set; }
            public IList<AttributeType> attributeTypes { get; set; }
            public IList<Document> documents { get; set; }
            public IList<Specification> specifications { get; set; }
            public IList<CrossSell> crossSells { get; set; }
            public IList<Accessory> accessories { get; set; }
            public IList<ProductUnitOfMeasure> productUnitOfMeasures { get; set; }
            public IList<ProductImage> productImages { get; set; }
            public Properties properties { get; set; }
            public int score { get; set; }
            public ScoreExplanation scoreExplanation { get; set; }
            public int searchBoost { get; set; }
            public int searchBoostDecimal { get; set; }
            public string salePriceLabel { get; set; }
            public bool cantBuy { get; set; }
            public Brand brand { get; set; }
            public ProductLine productLine { get; set; }
            public ProductSubscription productSubscription { get; set; }
            public string replacementProductId { get; set; }
            public IList<Warehouse> warehouses { get; set; }
            public IList<RelatedProduct> relatedProducts { get; set; }
            public IList<AlsoPurchasedProduct> alsoPurchasedProducts { get; set; }
        }

        public class Attention:  BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }



        public class BreakPrice:  BaseModel
        {
            public int breakQty { get; set; }
            public int breakPrice { get; set; }
            public string breakPriceDisplay { get; set; }
            public string savingsMessage { get; set; }
            public int breakPriceWithVat { get; set; }
            public string breakPriceWithVatDisplay { get; set; }
        }

        public class CardType:  BaseModel
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Carrier : BaseModel
        {
            public string id { get; set; }
            public string description { get; set; }
            public IList<ShipVia> shipVias { get; set; }
        }


        public class City : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class CompanyName : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class ConfigurationDto : BaseModel
        {
            public IList<Section> sections { get; set; }
            public bool hasDefaults { get; set; }
            public bool isKit { get; set; }
        }



        public class CreditCard : BaseModel
        {
            public string cardType { get; set; }
            public string cardHolderName { get; set; }
            public string cardNumber { get; set; }
            public int expirationMonth { get; set; }
            public int expirationYear { get; set; }
            public string securityCode { get; set; }
            public bool useBillingAddress { get; set; }
            public string address1 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string stateAbbreviation { get; set; }
            public string country { get; set; }
            public string countryAbbreviation { get; set; }
            public string postalCode { get; set; }
        }



        public class CrossSell : BaseModel
        {
        }

        public class CustomerOrderTaxis : BaseModel
        {
            public string taxCode { get; set; }
            public string taxDescription { get; set; }
            public int taxRate { get; set; }
            public int taxAmount { get; set; }
            public string taxAmountDisplay { get; set; }
            public int sortOrder { get; set; }
        }

        public class DetailedFieldScore : BaseModel
        {
            public string name { get; set; }
            public int score { get; set; }
            public int boost { get; set; }
            public string matchText { get; set; }
            public int termFrequencyNormalized { get; set; }
            public int inverseDocumentFrequency { get; set; }
            public bool scoreUsed { get; set; }
        }


        public class ECheck : BaseModel
        {
            public string accountHolder { get; set; }
            public string accountNumber { get; set; }
            public string routingNumber { get; set; }
            public bool useBillingAddress { get; set; }
            public string address1 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string stateAbbreviation { get; set; }
            public string country { get; set; }
            public string countryAbbreviation { get; set; }
            public string postalCode { get; set; }
        }

        public class Email : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class ExpirationMonth : BaseModel
        {
            public string key { get; set; }
            public int value { get; set; }
        }

        public class ExpirationYear : BaseModel
        {
            public int key { get; set; }
            public int value { get; set; }
        }

        public class Fax : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class FirstName : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class LastName : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class Option : BaseModel
        {
            public string sectionOptionId { get; set; }
            public string sectionName { get; set; }
            public string productName { get; set; }
            public string productId { get; set; }
            public string description { get; set; }
            public int price { get; set; }
            public bool userProductPrice { get; set; }
            public bool selected { get; set; }
            public int sortOrder { get; set; }
            public int quantity { get; set; }
        }

        public class Pagination
        {
            public int currentPage { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
            public int defaultPageSize { get; set; }
            public int totalItemCount { get; set; }
            public int numberOfPages { get; set; }
            public IList<int> pageSizeOptions { get; set; }
            public IList<SortOption> sortOptions { get; set; }
            public string sortType { get; set; }
            public string nextPageUri { get; set; }
            public string prevPageUri { get; set; }
        }

        public class ParentSpecification : BaseModel
        {
        }

        public class PaymentMethod : BaseModel
        {
            public string name { get; set; }
            public string description { get; set; }
            public bool isCreditCard { get; set; }
            public bool isECheck { get; set; }
            public bool isPaymentProfile { get; set; }
            public string cardType { get; set; }
            public string billingAddress { get; set; }
            public bool isPaymentProfileExpired { get; set; }
            public string tokenScheme { get; set; }
        }

        public class PaymentMethod2 : BaseModel
        {
            public string name { get; set; }
            public string description { get; set; }
            public bool isCreditCard { get; set; }
            public bool isECheck { get; set; }
            public bool isPaymentProfile { get; set; }
            public string cardType { get; set; }
            public string billingAddress { get; set; }
            public bool isPaymentProfileExpired { get; set; }
            public string tokenScheme { get; set; }
        }

        public class PaymentOptions : BaseModel
        {
            public IList<PaymentMethod> paymentMethods { get; set; }
            public IList<CardType> cardTypes { get; set; }
            public IList<ExpirationMonth> expirationMonths { get; set; }
            public IList<ExpirationYear> expirationYears { get; set; }
            public CreditCard creditCard { get; set; }
            public ECheck eCheck { get; set; }
            public bool canStorePaymentProfile { get; set; }
            public bool storePaymentProfile { get; set; }
            public bool isPayPal { get; set; }
            public string payPalPayerId { get; set; }
            public string payPalToken { get; set; }
            public string payPalPaymentUrl { get; set; }
        }

        public class Phone : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class PostalCode : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
        }

        public class Pricing : BaseModel
        {
            public string productId { get; set; }
            public bool isOnSale { get; set; }
            public bool requiresRealTimePrice { get; set; }
            public AdditionalResults additionalResults { get; set; }
            public int unitCost { get; set; }
            public string unitCostDisplay { get; set; }
            public int unitListPrice { get; set; }
            public string unitListPriceDisplay { get; set; }
            public int extendedUnitListPrice { get; set; }
            public string extendedUnitListPriceDisplay { get; set; }
            public int unitRegularPrice { get; set; }
            public string unitRegularPriceDisplay { get; set; }
            public int extendedUnitRegularPrice { get; set; }
            public string extendedUnitRegularPriceDisplay { get; set; }
            public int unitNetPrice { get; set; }
            public string unitNetPriceDisplay { get; set; }
            public int extendedUnitNetPrice { get; set; }
            public string extendedUnitNetPriceDisplay { get; set; }
            public string unitOfMeasure { get; set; }
            public int vatRate { get; set; }
            public int vatAmount { get; set; }
            public string vatAmountDisplay { get; set; }
            public IList<UnitListBreakPrice> unitListBreakPrices { get; set; }
            public IList<UnitRegularBreakPrice> unitRegularBreakPrices { get; set; }
            public int regularPrice { get; set; }
            public string regularPriceDisplay { get; set; }
            public int extendedRegularPrice { get; set; }
            public string extendedRegularPriceDisplay { get; set; }
            public int actualPrice { get; set; }
            public string actualPriceDisplay { get; set; }
            public int extendedActualPrice { get; set; }
            public string extendedActualPriceDisplay { get; set; }
            public IList<RegularBreakPrice> regularBreakPrices { get; set; }
            public IList<ActualBreakPrice> actualBreakPrices { get; set; }
            public int unitListPriceWithVat { get; set; }
            public string unitListPriceWithVatDisplay { get; set; }
            public int extendedUnitListPriceWithVat { get; set; }
            public string extendedUnitListPriceWithVatDisplay { get; set; }
            public int unitRegularPriceWithVat { get; set; }
            public string unitRegularPriceWithVatDisplay { get; set; }
            public int extendedUnitRegularPriceWithVat { get; set; }
            public string extendedUnitRegularPriceWithVatDisplay { get; set; }
            public int vatMinusExtendedUnitRegularPrice { get; set; }
        }

        public class ProductDto : BaseModel
        {
        }

        public class ProductImage : BaseModel
        {
            public string id { get; set; }
            public int sortOrder { get; set; }
            public string name { get; set; }
            public string imageType { get; set; }
            public string smallImagePath { get; set; }
            public string mediumImagePath { get; set; }
            public string largeImagePath { get; set; }
            public string altText { get; set; }
        }

        public class ProductLine : BaseModel
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class ProductSubscription : BaseModel
        {
            public bool subscriptionAddToInitialOrder { get; set; }
            public bool subscriptionAllMonths { get; set; }
            public bool subscriptionApril { get; set; }
            public bool subscriptionAugust { get; set; }
            public string subscriptionCyclePeriod { get; set; }
            public bool subscriptionDecember { get; set; }
            public bool subscriptionFebruary { get; set; }
            public bool subscriptionFixedPrice { get; set; }
            public bool subscriptionJanuary { get; set; }
            public bool subscriptionJuly { get; set; }
            public bool subscriptionJune { get; set; }
            public bool subscriptionMarch { get; set; }
            public bool subscriptionMay { get; set; }
            public bool subscriptionNovember { get; set; }
            public bool subscriptionOctober { get; set; }
            public int subscriptionPeriodsPerCycle { get; set; }
            public bool subscriptionSeptember { get; set; }
            public string subscriptionShipViaId { get; set; }
            public int subscriptionTotalCycles { get; set; }
        }

        public class ProductUnitOfMeasure : BaseModel
        {
            public string productUnitOfMeasureId { get; set; }
            public string unitOfMeasure { get; set; }
            public string unitOfMeasureDisplay { get; set; }
            public string description { get; set; }
            public int qtyPerBaseUnitOfMeasure { get; set; }
            public string roundingRule { get; set; }
            public bool isDefault { get; set; }
            public Availability availability { get; set; }
        }

        public class Properties : BaseModel
        {
        }

        public class RegularBreakPrice : BaseModel
        {
            public int breakQty { get; set; }
            public int breakPrice { get; set; }
            public string breakPriceDisplay { get; set; }
            public string savingsMessage { get; set; }
            public int breakPriceWithVat { get; set; }
            public string breakPriceWithVatDisplay { get; set; }
        }

        public class RelatedProduct : BaseModel
        {
            public string relatedProductType { get; set; }
            public ProductDto productDto { get; set; }
        }

        public class Root : BaseModel
        {
            public string uri { get; set; }
            public IList<CartCollection> cartCollection { get; set; }
            public Pagination pagination { get; set; }
            public Properties properties { get; set; }
        }

        public class ScoreExplanation : BaseModel
        {
            public int totalBoost { get; set; }
            public IList<AggregateFieldScore> aggregateFieldScores { get; set; }
            public IList<DetailedFieldScore> detailedFieldScores { get; set; }
        }

        public class Section : BaseModel
        {
            public string sectionName { get; set; }
            public IList<Option> options { get; set; }
        }

        public class SectionOption : BaseModel
        {
            public string sectionOptionId { get; set; }
            public string sectionName { get; set; }
            public string optionName { get; set; }
        }

        public class ShipTo : BaseModel
        {
            public string uri { get; set; }
            public bool isNew { get; set; }
            public bool oneTimeAddress { get; set; }
            public string label { get; set; }
            public Validation validation { get; set; }
            public bool isDefault { get; set; }
            public string id { get; set; }
            public string customerNumber { get; set; }
            public string customerSequence { get; set; }
            public string customerName { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string contactFullName { get; set; }
            public string companyName { get; set; }
            public string attention { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string address4 { get; set; }
            public string city { get; set; }
            public string postalCode { get; set; }
            public State state { get; set; }
            public Country country { get; set; }
            public string phone { get; set; }
            public string fullAddress { get; set; }
            public string email { get; set; }
            public string fax { get; set; }
            public bool isVmiLocation { get; set; }
            public Properties properties { get; set; }
        }

        public class ShipTo2 : BaseModel
        {
            public string uri { get; set; }
            public bool isNew { get; set; }
            public bool oneTimeAddress { get; set; }
            public string label { get; set; }
            public Validation validation { get; set; }
            public bool isDefault { get; set; }
            public string id { get; set; }
            public string customerNumber { get; set; }
            public string customerSequence { get; set; }
            public string customerName { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string contactFullName { get; set; }
            public string companyName { get; set; }
            public string attention { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string address4 { get; set; }
            public string city { get; set; }
            public string postalCode { get; set; }
            public State state { get; set; }
            public Country country { get; set; }
            public string phone { get; set; }
            public string fullAddress { get; set; }
            public string email { get; set; }
            public string fax { get; set; }
            public bool isVmiLocation { get; set; }
            public Properties properties { get; set; }
        }

        public class ShipVia : BaseModel
        {
            public string id { get; set; }
            public string description { get; set; }
            public bool isDefault { get; set; }
        }

        public class ShipVia2 : BaseModel
        {
            public string id { get; set; }
            public string description { get; set; }
            public bool isDefault { get; set; }
        }

        public class SortOption : BaseModel
        {
            public string displayName { get; set; }
            public string sortType { get; set; }
        }

        public class Specification : BaseModel
        {
            public string specificationId { get; set; }
            public string name { get; set; }
            public string nameDisplay { get; set; }
            public string value { get; set; }
            public string description { get; set; }
            public int sortOrder { get; set; }
            public bool isActive { get; set; }
            public ParentSpecification parentSpecification { get; set; }
            public string htmlContent { get; set; }
            public IList<Specification> specifications { get; set; }
        }

        public class State : BaseModel
        {
            public bool isRequired { get; set; }
            public bool isDisabled { get; set; }
            public int maxLength { get; set; }
            public string uri { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string abbreviation { get; set; }
            public Properties properties { get; set; }
        }

        public class State3 : BaseModel
        {
            public string uri { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string abbreviation { get; set; }
            public Properties properties { get; set; }
        }

        public class StyledProduct : BaseModel
        {
            public string productId { get; set; }
            public string name { get; set; }
            public string shortDescription { get; set; }
            public string erpNumber { get; set; }
            public string customerName { get; set; }
            public string mediumImagePath { get; set; }
            public string smallImagePath { get; set; }
            public string largeImagePath { get; set; }
            public int qtyOnHand { get; set; }
            public string unitOfMeasure { get; set; }
            public bool trackInventory { get; set; }
            public int minimumOrderQty { get; set; }
            public string productDetailUrl { get; set; }
            public int numberInCart { get; set; }
            public Pricing pricing { get; set; }
            public bool quoteRequired { get; set; }
            public IList<StyleValue> styleValues { get; set; }
            public Availability availability { get; set; }
            public IList<ProductUnitOfMeasure> productUnitOfMeasures { get; set; }
            public IList<ProductImage> productImages { get; set; }
            public IList<Warehouse> warehouses { get; set; }
            public string manufacturerItem { get; set; }
            public string upcCode { get; set; }
            public string sku { get; set; }
            public bool cantBuy { get; set; }
        }

        public class StyleTrait : BaseModel
        {
            public string styleTraitId { get; set; }
            public string name { get; set; }
            public string nameDisplay { get; set; }
            public string unselectedValue { get; set; }
            public int sortOrder { get; set; }
            public IList<StyleValue> styleValues { get; set; }
        }

        public class StyleValue : BaseModel
        {
            public string styleTraitName { get; set; }
            public string styleTraitId { get; set; }
            public string styleTraitValueId { get; set; }
            public string value { get; set; }
            public string valueDisplay { get; set; }
            public int sortOrder { get; set; }
            public bool isDefault { get; set; }
        }

        public class UnitListBreakPrice : BaseModel
        {
            public int breakQty { get; set; }
            public int breakPrice { get; set; }
            public string breakPriceDisplay { get; set; }
            public string savingsMessage { get; set; }
            public int breakPriceWithVat { get; set; }
            public string breakPriceWithVatDisplay { get; set; }
        }

        public class UnitRegularBreakPrice : BaseModel
        {
            public int breakQty { get; set; }
            public int breakPrice { get; set; }
            public string breakPriceDisplay { get; set; }
            public string savingsMessage { get; set; }
            public int breakPriceWithVat { get; set; }
            public string breakPriceWithVatDisplay { get; set; }
        }

        public class Validation : BaseModel
        {
            public FirstName firstName { get; set; }
            public LastName lastName { get; set; }
            public CompanyName companyName { get; set; }
            public Attention attention { get; set; }
            public Address1 address1 { get; set; }
            public Address2 address2 { get; set; }
            public Address3 address3 { get; set; }
            public Address4 address4 { get; set; }
            public Country country { get; set; }
            public State state { get; set; }
            public City city { get; set; }
            public PostalCode postalCode { get; set; }
            public Phone phone { get; set; }
            public Email email { get; set; }
            public Fax fax { get; set; }
        }

        public class Warehouse : BaseModel
        {
            public string id { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string countryId { get; set; }
            public string description { get; set; }
            public bool isDefault { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string postalCode { get; set; }
            public string shipSite { get; set; }
            public string state { get; set; }
            public int messageType { get; set; }
            public string message { get; set; }
            public bool requiresRealTimeInventory { get; set; }
            public int qty { get; set; }
            public Properties properties { get; set; }
        }

    }

    

    


}

