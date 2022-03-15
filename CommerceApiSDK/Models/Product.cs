using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class SortOption
    {
        /// <summary>Gets or sets the sort string to display in UI.</summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the sort types (SortOrderType constants) in order to show in UI - lowest is default. pass this value to SortBy as a
        /// string.
        /// </summary>
        public string SortType { get; set; }
    }

    public class Pagination
    {
        /// <summary>Gets or sets the current page.</summary>
        [Obsolete]
        public int CurrentPage { get; set; }

        /// <summary>Gets or sets the page.</summary>
        public int Page { get; set; }

        /// <summary>Gets or sets the size of the page.</summary>
        public int PageSize { get; set; }

        /// <summary>Gets or sets the default size of the page.</summary>
        public int DefaultPageSize { get; set; }

        /// <summary>Gets or sets the total item count.</summary>
        public int TotalItemCount { get; set; }

        /// <summary>Gets or sets the number of pages.</summary>
        public int NumberOfPages { get; set; }

        /// <summary>Gets or sets the page size options.</summary>
        public IList<int> PageSizeOptions { get; set; }

        /// <summary>Gets or sets the sort options.</summary>
        public IList<SortOption> SortOptions { get; set; }

        /// <summary>Gets or sets the type of the sort.</summary>
        public string SortType { get; set; }

        /// <summary>full url to rest endpoint to retrieve next page or null if no next page</summary>
        public string NextPageUri { get; set; }

        /// <summary>full url to rest endpoint to retrieve previous page or null if no previous page</summary>
        public string PrevPageUri { get; set; }
    }

    public class ProductPriceDto
    {
        public Guid ProductId { get; set; }

        /// <summary>Gets or sets a value indicating whether the Product is calculated to be on sale</summary>
        public bool IsOnSale { get; set; }

        /// <summary>Get or sets whether this pricing is empty and requires call to fetch realtime prices</summary>
        public bool RequiresRealTimePrice { get; set; } = false;

        /// <summary>Gets or sets a value indicating whether quote required.</summary>
        public bool QuoteRequired { get; set; }

        /// <summary>Gets or sets the user definable additional results returned from price calculation</summary>
        public Dictionary<string, string> AdditionalResults { get; set; }

        /// <summary>Gets or sets the unit cost.</summary>
        public decimal UnitCost { get; set; }

        /// <summary>Gets or sets the unit cost display.</summary>
        public string UnitCostDisplay { get; set; }

        /// <summary>Gets or sets the unit list price.</summary>
        public decimal UnitListPrice { get; set; }

        /// <summary>Gets or sets the unit list price display.</summary>
        public string UnitListPriceDisplay { get; set; }

        /// <summary>Gets or sets the Quantity UnitListPrice</summary>
        public decimal ExtendedUnitListPrice { get; set; }

        /// <summary>Gets or sets the Formatted Quantity ListPrice</summary>
        public string ExtendedUnitListPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit regular price.</summary>
        public decimal UnitRegularPrice { get; set; }

        /// <summary>Gets or sets the unit regular price display.</summary>
        public string UnitRegularPriceDisplay { get; set; }

        /// <summary>Gets or sets the Quantity UnitRegularPrice</summary>
        public decimal ExtendedUnitRegularPrice { get; set; }

        /// <summary>Gets or sets the Formatted Quantity UnitRegularPrice</summary>
        public string ExtendedUnitRegularPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit net price.</summary>
        public decimal UnitNetPrice { get; set; }

        /// <summary>Gets or sets the unit net price display.</summary>
        public string UnitNetPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the Quantity UnitNetPrice</summary>
        public decimal ExtendedUnitNetPrice { get; set; }

        /// <summary>Gets or sets the Formatted Quantity UnitNetPrice</summary>
        public string ExtendedUnitNetPriceDisplay { get; set; }

        /// <summary>Gets or sets the calculated break quantity unit list prices</summary>
        public IList<BreakPriceDto> UnitListBreakPrices { get; set; }

        /// <summary>Gets or sets the calculated break quantity unit regular prices</summary>
        public IList<BreakPriceDto> UnitRegularBreakPrices { get; set; }
    }

    public class ConfigSectionDto
    {
        public string SectionName { get; set; }

        public IList<ConfigSectionOptionDto> Options { get; set; }

        // for V2
        public Guid Id { get; set; }

        public int SortOrder { get; set; }
    }

    public class ConfigSectionOptionDto
    {
        public Guid SectionOptionId { get; set; }

        public string SectionName { get; set; }

        public string ProductName { get; set; }

        public Guid? ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool UserProductPrice { get; set; }

        public bool Selected { get; set; }

        public int SortOrder { get; set; }

        // for V2
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }
    }

    public class LegacyConfigurationDto
    {
        public IList<ConfigSectionDto> Sections { get; set; }

        public bool HasDefaults { get; set; }

        public bool IsKit { get; set; }
    }

    public class AvailabilityDto
    {
        public int MessageType { get; set; }

        public string Message { get; set; }

        public bool RequiresRealTimeInventory { get; set; }
    }

    public class StyleTrait
    {
        public Guid StyleTraitId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public string UnselectedValue { get; set; }

        public int SortOrder { get; set; }

        public IList<StyleValue> StyleValues { get; set; }

        // for V2
        public Guid Id { get; set; }

        public IList<StyleValue> TraitValues { get; set; }
    }

    public class StyleValue
    {
        public string StyleTraitName { get; set; }

        public Guid StyleTraitId { get; set; }

        public Guid StyleTraitValueId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }

        public int SortOrder { get; set; }

        public bool IsDefault { get; set; }

        // for V2
        public Guid Id { get; set; }
    }

    public class ProductUnitOfMeasure
    {
        public Guid ProductUnitOfMeasureId { get; set; }

        public string UnitOfMeasure { get; set; }

        public string UnitOfMeasureDisplay { get; set; }

        public string Description { get; set; }

        public double QtyPerBaseUnitOfMeasure { get; set; }

        public string RoundingRule { get; set; }

        public bool IsDefault { get; set; }

        public AvailabilityDto Availability { get; set; }
    }

    public class StyledProduct : BaseModel
    {
        /// <summary>Gets or sets the product identifier.</summary>
        public Guid ProductId { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the short description.</summary>
        public string ShortDescription { get; set; }

        /// <summary>Gets or sets the erp number.</summary>
        public string ERPNumber { get; set; }

        /// <summary>Gets or sets the medium image path.</summary>
        public string MediumImagePath { get; set; }

        /// <summary>Gets or sets the small image path.</summary>
        public string SmallImagePath { get; set; }

        /// <summary>Gets or sets the large image path.</summary>
        public string LargeImagePath { get; set; }

        /// <summary>Gets or sets the qty on hand.</summary>
        public decimal QtyOnHand { get; set; }

        /// <summary>Gets or sets the number in cart.</summary>
        [Obsolete]
        public decimal NumberInCart { get; set; }

        /// <summary>Gets or sets the pricing.</summary>
        public ProductPriceDto Pricing { get; set; }

        /// <summary>Gets or sets a value indicating whether [quote required].</summary>
        public bool QuoteRequired { get; set; }

        /// <summary>Gets or sets the style values.</summary>
        public IList<StyleValue> StyleValues { get; set; }

        /// <summary>Gets or sets the availability.</summary>
        public AvailabilityDto Availability { get; set; }

        /// <summary>Gets or sets the list of all available units of measure.</summary>
        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        /// <summary>Gets or sets the list of all product images.</summary>
        public IList<ProductImage> ProductImages { get; set; }

        /// <summary>Gets or sets the warehouses.</summary>
        public IList<WarehouseDto> Warehouses { get; set; } = new List<WarehouseDto>();

        public bool TrackInventory { get; set; }
    }

    public class AttributeValue
    {
        public Guid AttributeValueId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }

        // for V2
        public Guid Id { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }

    public class AttributeType
    {
        public Guid AttributeTypeId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public int Sort { get; set; }

        public IList<AttributeValue> AttributeValueFacets { get; set; }

        // for V2
        public Guid Id { get; set; }

        public string Label { get; set; }

        public bool IsFilter { get; set; }

        public bool IsComparable { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public IList<AttributeValue> AttributeValues { get; set; }
    }

    public class Document
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string FilePath { get; set; }

        [Obsolete("Use FilePath instead")]
        public string FileUrl { get; set; }

        public string DocumentType { get; set; }

        public Guid? LanguageId { get; set; }

        [Obsolete("Use DocumentType instead")]
        public string FileTypeString { get; set; }
    }

    public class Specification
    {
        public Guid SpecificationId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public double SortOrder { get; set; }

        public bool IsActive { get; set; }

        public Specification ParentSpecification { get; set; }

        public string HtmlContent { get; set; }

        public Specification Specifications { get; set; }
    }

    public class ProductImage
    {
        public int SortOrder { get; set; }

        public string Name { get; set; }

        public string SmallImagePath { get; set; }

        public string MediumImagePath { get; set; }

        public string LargeImagePath { get; set; }

        public string AltText { get; set; }

        public string ImageType { get; set; }
    }

    public class ProductSubscription
    {
        public bool SubscriptionAddToInitialOrder { get; set; }

        public bool SubscriptionAllMonths { get; set; }

        public bool SubscriptionApril { get; set; }

        public bool SubscriptionAugust { get; set; }

        public string SubscriptionCyclePeriod { get; set; }

        public bool SubscriptionDecember { get; set; }

        public bool SubscriptionFebruary { get; set; }

        public bool SubscriptionFixedPrice { get; set; }

        public bool SubscriptionJanuary { get; set; }

        public bool SubscriptionJuly { get; set; }

        public bool SubscriptionJune { get; set; }

        public bool SubscriptionMarch { get; set; }

        public bool SubscriptionMay { get; set; }

        public bool SubscriptionNovember { get; set; }

        public bool SubscriptionOctober { get; set; }

        public int SubscriptionPeriodsPerCycle { get; set; }

        public bool SubscriptionSeptember { get; set; }

        public Guid? SubscriptionShipViaId { get; set; }

        public int SubscriptionTotalCycles { get; set; }
    }

    public class Product : BaseModel
    {
        /// <summary>Gets or sets the product id.</summary>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the order line identifier.</summary>
        public Guid? OrderLineId { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the customer product name.</summary>
        public string CustomerName { get; set; }

        /// <summary>Gets or sets the short description.</summary>
        public string ShortDescription { get; set; }

        /// <summary>Gets or sets the erp number.</summary>
        // ReSharper disable once InconsistentNaming
        public string ERPNumber { get; set; }

        /// <summary>Gets or sets the erp description.</summary>
        // ReSharper disable once InconsistentNaming
        public string ERPDescription { get; set; }

        /// <summary>Gets or sets the URL segment used for pathing.</summary>
        public string UrlSegment { get; set; }

        /// <summary>Gets or sets the basic list price.</summary>
        public decimal BasicListPrice { get; set; }

        /// <summary>Gets or sets the basic sale price.</summary>
        public decimal BasicSalePrice { get; set; }

        /// <summary>Gets or sets the basic sale start date.</summary>
        public DateTime? BasicSaleStartDate { get; set; }

        /// <summary>Gets or sets the basic sale end date.</summary>
        public DateTime? BasicSaleEndDate { get; set; }

        /// <summary>Gets or sets the small image url.</summary>
        public string SmallImagePath { get; set; }

        /// <summary>Gets or sets the medium image url .</summary>
        public string MediumImagePath { get; set; }

        /// <summary>Gets or sets the large image url.</summary>
        public string LargeImagePath { get; set; }

        /// <summary>Gets or sets the product pricing data.</summary>
        public ProductPriceDto Pricing { get; set; }

        /// <summary>Gets or sets the currency symbol.</summary>
        public virtual string CurrencySymbol { get; set; }

        /// <summary>Gets or sets the inventory quantity.</summary>
        public decimal QtyOnHand { get; set; }

        /// <summary>Gets or sets whether the product is configurable.</summary>
        public bool IsConfigured { get; set; }

        /// <summary>Gets or sets whether the product is has a fixed configuration.</summary>
        public bool IsFixedConfiguration { get; set; }

        /// <summary>Gets or sets whether the product is active.</summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets whether the product is hazardous.</summary>
        public bool IsHazardousGood { get; set; }

        /// <summary>Gets or sets whether the product is discontinued.</summary>
        public bool IsDiscontinued { get; set; }

        /// <summary>Gets or sets whether the product is special ordered.</summary>
        public bool IsSpecialOrder { get; set; }

        /// <summary>Gets or sets whether the product is a gift card.</summary>
        public bool IsGiftCard { get; set; }

        /// <summary>Gets or sets whether the product is being compared (client side flag).</summary>
        public bool IsBeingCompared { get; set; }

        /// <summary>Gets or sets whether the product is sponsored.</summary>
        public bool IsSponsored { get; set; }

        /// <summary>Gets or sets whether the product is subscription.</summary>
        public bool IsSubscription { get; set; }

        /// <summary>Gets or sets whether the product requires a quote.</summary>
        public bool QuoteRequired { get; set; }

        /// <summary>Gets or sets the manufacturer item number.</summary>
        public string ManufacturerItem { get; set; }

        /// <summary>Gets or sets the packing description.</summary>
        public string PackDescription { get; set; }

        /// <summary>Gets or sets the image alt text.</summary>
        public string AltText { get; set; }

        /// <summary>Gets or sets the customer specific unit of measure.</summary>
        public string CustomerUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the ability to order with no inventory.</summary>
        public bool CanBackOrder { get; set; }

        /// <summary>Gets or sets whether the inventory is tracked.</summary>
        public bool TrackInventory { get; set; }

        /// <summary>Gets or sets the multiple sale quantity.</summary>
        public int MultipleSaleQty { get; set; }

        /// <summary>Gets or sets the minimum order quantity.</summary>
        public int MinimumOrderQty { get; set; }

        /// <summary>Gets or sets the html content for the product.</summary>
        public string HtmlContent { get; set; }

        /// <summary>Gets or sets the product code.</summary>
        public string ProductCode { get; set; }

        /// <summary>Gets or sets the price code.</summary>
        public string PriceCode { get; set; }

        /// <summary>Gets or sets the sku.</summary>
        public string Sku { get; set; }

        /// <summary>Gets or sets the upc code.</summary>
        public string UPCCode { get; set; }

        /// <summary>Gets or sets the model number.</summary>
        public string ModelNumber { get; set; }

        /// <summary>Gets or sets the tax code 1.</summary>
        public string TaxCode1 { get; set; }

        /// <summary>Gets or sets the tax code 2.</summary>
        public string TaxCode2 { get; set; }

        /// <summary>Gets or sets the tax category.</summary>
        public string TaxCategory { get; set; }

        /// <summary>Gets or sets the shipping classification.</summary>
        public string ShippingClassification { get; set; }

        /// <summary>Gets or sets the shipping length.</summary>
        public string ShippingLength { get; set; }

        /// <summary>Gets or sets the shipping width.</summary>
        public string ShippingWidth { get; set; }

        /// <summary>Gets or sets the shipping height.</summary>
        public string ShippingHeight { get; set; }

        /// <summary>Gets or sets the shipping wieght.</summary>
        public string ShippingWeight { get; set; }

        /// <summary>Gets or sets the shipping quantity per package.</summary>
        public decimal QtyPerShippingPackage { get; set; }

        /// <summary>Gets or sets the shipping amount override.</summary>
        public decimal? ShippingAmountOverride { get; set; }

        /// <summary>Gets or sets the handling amount override.</summary>
        public decimal? HandlingAmountOverride { get; set; }

        /// <summary>Gets or sets the meta description for the product detail page.</summary>
        public string MetaDescription { get; set; }

        /// <summary>Gets or sets the meta keywords for the product detail page.</summary>
        public string MetaKeywords { get; set; }

        /// <summary>Gets or sets the page title for the product detail page.</summary>
        public string PageTitle { get; set; }

        /// <summary>Gets or sets whether to allow any gift card amount.</summary>
        public bool AllowAnyGiftCardAmount { get; set; }

        /// <summary>Gets or sets the sort order of the product.</summary>
        public int SortOrder { get; set; }

        /// <summary>Gets or sets whether the product has msds.</summary>
        public bool HasMsds { get; set; }

        /// <summary>Gets or sets the unspsc.</summary>
        public string Unspsc { get; set; }

        /// <summary>Gets or sets the rounding rule.</summary>
        public string RoundingRule { get; set; }

        /// <summary>Gets or sets the vendor number.</summary>
        public string VendorNumber { get; set; }

        /// <summary>Gets or sets the configuration.</summary>
        public LegacyConfigurationDto ConfigurationDto { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the unit of measure display text.</summary>
        public string UnitOfMeasureDisplay { get; set; }

        /// <summary>Gets or sets the unit of measure description.</summary>
        public string UnitOfMeasureDescription { get; set; }

        /// <summary>Gets or sets the selected unit of measure.</summary>
        public string SelectedUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the selected unit of measure.</summary>
        public string SelectedUnitOfMeasureDisplay { get; set; }

        /// <summary>Gets or sets the full url to the detail page.</summary>
        public string ProductDetailUrl { get; set; }

        /// <summary>Gets or sets whether the product can be added to cart.</summary>
        public bool CanAddToCart { get; set; }

        /// <summary>Gets or sets whether the website allows adding to cart.</summary>
        public bool AllowedAddToCart { get; set; }

        /// <summary>Gets or sets whether the product can be added to a wishlist.</summary>
        public bool CanAddToWishlist { get; set; }

        /// <summary>Gets or sets whether the product view details button should be displayed.</summary>
        public bool CanViewDetails { get; set; }

        /// <summary>Gets or sets whether the product price can be shown.</summary>
        public bool CanShowPrice { get; set; }

        /// <summary>Gets or sets whether the product unit of measure can be shown.</summary>
        public bool CanShowUnitOfMeasure { get; set; }

        /// <summary>Gets or sets whether the product quantity can be entered.</summary>
        public bool CanEnterQuantity { get; set; }

        /// <summary>Gets or sets whether the product can be configured with the configurator.</summary>
        public bool CanConfigure { get; set; }

        /// <summary>Gets or sets whether the product has styled child products.</summary>
        public bool IsStyleProductParent { get; set; }

        /// <summary>Gets or sets the ID of the parent of this product, if any.</summary>
        public Guid? StyleParentId { get; set; }

        public bool RequiresRealTimeInventory { get; set; }

        /// <summary>Gets or sets the quantity of the product already in the cart.</summary>
        [Obsolete]
        public decimal NumberInCart { get; set; }

        /// <summary>Gets or sets the quantity ordered (for adding to cart).</summary>
        [Obsolete]
        public decimal QtyOrdered { get; set; }

        /// <summary>Gets or sets the inventory availability information.</summary>
        public AvailabilityDto Availability { get; set; }

        /// <summary>Gets or sets the style traits.</summary>
        public IList<StyleTrait> StyleTraits { get; set; }

        /// <summary>Gets or sets the style product children.</summary>
        public IList<StyledProduct> StyledProducts { get; set; }

        /// <summary>Gets or sets the attributes assigned to the product.</summary>
        public IList<AttributeType> AttributeTypes { get; set; }

        /// <summary>Gets or sets the documents.</summary>
        public IList<Document> Documents { get; set; }

        /// <summary>Gets or sets the specifications.</summary>
        public IList<Specification> Specifications { get; set; }

        /// <summary>Gets or sets the product cross sells.</summary>
        public IList<Product> CrossSells { get; set; }

        /// <summary>Gets or sets the accessories.</summary>
        public IList<Product> Accessories { get; set; }

        /// <summary>Gets or sets the list of all available units of measure.</summary>
        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        /// <summary>Gets or sets the list of all product images.</summary>
        public IList<ProductImage> ProductImages { get; set; }

        /// <summary>The search provider score for this product</summary>
        public double Score { get; set; }

        /// <summary>Gets or sets the index time search boost</summary>
        public int SearchBoost { get; set; }

        /// <summary>Gets or sets the sale price label.</summary>
        public string SalePriceLabel { get; set; }

        /// <summary>Gets or sets the product subscription.</summary>
        public ProductSubscriptionDto ProductSubscription { get; set; }

        /// <summary>Gets or sets the replacement product id.</summary>
        public Guid? ReplacementProductId { get; set; }

        /// <summary>Gets or sets the warehouses.</summary>
        public IList<WarehouseDto> Warehouses { get; set; }

        public Brand Brand { get; set; }

        // for V2
        public string ProductNumber { get; set; }

        public string CustomerProductNumber { get; set; }

        public string ProductTitle { get; set; }

        public string CanonicalUrl { get; set; }

        public decimal UnitListPrice { get; set; }

        public string UnitListPriceDisplay { get; set; }

        public int? PriceFacet { get; set; }

        public string ImageAltText { get; set; }

        public string ConfigurationType { get; set; }

        public bool IsVariantParent { get; set; }

        public Guid? VariantTypeId { get; set; }

        public bool CantBuy { get; set; }

        public ProductLineFacetDto ProductLine { get; set; }

        public IList<ProductUnitOfMeasure> UnitOfMeasures { get; set; }

        public ScoreExplanation ScoreExplanation { get; set; }

        public ProductDetail Detail { get; set; }

        public ProductContent Content { get; set; }

        public IList<ProductImage> Images { get; set; }

        public IList<StyleTrait> VariantTraits { get; set; }

        public IList<ChildTraitValue> ChildTraitValues { get; set; }
    }

    public class CategoryFacetDto
    {
        public Guid CategoryId { get; set; }

        public Guid WebsiteId { get; set; }

        public string ShortDescription { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }

        public IList<CategoryFacetDto> SubCategoryDtos { get; set; }
    }

    public class BrandFacetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }

    public class ProductLineFacetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }

    public class SuggestionDto
    {
        public string HighlightedSuggestion { get; set; }

        public double Score { get; set; }

        public string Suggestion { get; set; }
    }

    public class WarehouseDto : AvailabilityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Qty { get; set; }

        public Properties Properties { get; set; }

        // for V2
        public Guid Id { get; set; }

        public decimal QtyAvailable { get; set; }
    }

    public class ProductInventoryDto
    {
        public Guid ProductId { get; set; }

        public decimal QtyOnHand { get; set; }

        public List<InventoryAvailabilityDto> InventoryAvailabilityDtos { get; set; }

        public List<InventoryWarehousesDto> InventoryWarehousesDtos { get; set; }

        public Dictionary<string, string> AdditionalResults { get; set; }
    }

    public class InventoryAvailabilityDto
    {
        public string UnitOfMeasure { get; set; }

        public AvailabilityDto Availability { get; set; }
    }

    public class InventoryWarehousesDto
    {
        public string UnitOfMeasure { get; set; }

        public List<WarehouseDto> WarehouseDtos { get; set; }
    }

    public class ProductDetail
    {
        public string Name { get; set; }

        public string ModelNumber { get; set; }

        public string Sku { get; set; }

        public string UpcCode { get; set; }

        public string Unspsc { get; set; }

        public string ProductCode { get; set; }

        public string PriceCode { get; set; }

        public int SortOrder { get; set; }

        public int MultipleSaleQty { get; set; }

        public bool CanBackOrder { get; set; }

        public string RoundingRule { get; set; }

        public Guid? ReplacementProductId { get; set; }

        public bool IsHazardousGood { get; set; }

        public bool HasMsds { get; set; }

        public bool IsSpecialOrder { get; set; }

        public bool IsGiftCard { get; set; }

        public bool AllowAnyGiftCardAmount { get; set; }

        public string TaxCode1 { get; set; }

        public string TaxCode2 { get; set; }

        public string TaxCategory { get; set; }

        public Guid? VatCodeId { get; set; }

        public string ShippingClassification { get; set; }

        public decimal ShippingLength { get; set; }

        public decimal ShippingWidth { get; set; }

        public decimal ShippingHeight { get; set; }

        public decimal ShippingWeight { get; set; }

        public LegacyConfigurationDto Configuration { get; set; }
    }

    public class ProductContent
    {
        public string HtmlContent { get; set; }

        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string OpenGraphTitle { get; set; }

        public string OpenGraphUrl { get; set; }

        public string OpenGraphImage { get; set; }
    }

    public class ScoreExplanation
    {
        public float TotalBoost { get; set; }

        public IList<FieldScore> AggregateFieldScores { get; set; }

        public IList<FieldScoreDetailed> DetailedFieldScores { get; set; }
    }

    public class FieldScoreDetailed : FieldScore
    {
        public float Boost { get; set; }

        public string MatchText { get; set; }

        public float TermFrequencyNormalized { get; set; }

        public float InverseDocumentFrequency { get; set; }

        public bool ScoreUsed { get; set; }
    }

    public class FieldScore
    {
        public string Name { get; set; }

        public float Score { get; set; }
    }

    public class ChildTraitValue
    {
        public Guid Id { get; set; }

        public Guid StyleTraitId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }
    }

    public class PriceRange
    {
        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }

        public int Count { get; set; }

        public IList<PriceFacet> PriceFacets { get; set; }
    }

    public class PriceFacet
    {
        public int MinimumPrice { get; set; }

        public int MaximumPrice { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }
}
