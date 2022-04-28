using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
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
        public ProductPrice Pricing { get; set; }

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
        public LegacyConfiguration ConfigurationDto { get; set; }

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
        public Availability Availability { get; set; }

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
        public IList<InventoryWarehouse> Warehouses { get; set; }

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

        public ProductLine ProductLine { get; set; }

        public IList<ProductUnitOfMeasure> UnitOfMeasures { get; set; }

        public ScoreExplanation ScoreExplanation { get; set; }

        public ProductDetail Detail { get; set; }

        public ProductContent Content { get; set; }

        public IList<ProductImage> Images { get; set; }

        public IList<StyleTrait> VariantTraits { get; set; }

        public IList<ChildTraitValue> ChildTraitValues { get; set; }
    }
}
