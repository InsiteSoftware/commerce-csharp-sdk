using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    /// <summary>
    /// Subset of cartline needed to add a new cartline
    /// </summary>
    public class AddCartLine : BaseModel
    {
        public Guid? ProductId { get; set; }

        public decimal? QtyOrdered { get; set; }

        public string UnitOfMeasure { get; set; }

        public string Notes { get; set; }

        public IList<SectionOptionDto> SectionOptions { get; set; }
    }

    public class CartLineList : BaseModel
    {
        public IList<CartLine> CartLines { get; set; }
    }

    public class CartLine : AddCartLine
    {
        /// <summary>Gets or sets the product URI.</summary>
        public string ProductUri { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the line.</summary>
        public int? Line { get; set; }

        /// <summary>
        /// Gets or sets the requisition identifier.
        /// Only used when adding to the cart and includes the Id of the Requisition the line came from.
        /// </summary>
        public Guid? RequisitionId { get; set; }

        public string SmallImagePath { get; set; }

        public string AltText { get; set; }

        public string ProductName { get; set; }

        public string ManufacturerItem { get; set; }

        public string CustomerName { get; set; }

        public string ShortDescription { get; set; }

        public string ErpNumber { get; set; }

        public string UnitOfMeasureDisplay { get; set; }

        public string UnitOfMeasureDescription { get; set; }

        public string BaseUnitOfMeasure { get; set; }

        public string BaseUnitOfMeasureDisplay { get; set; }

        public decimal QtyPerBaseUnitOfMeasure { get; set; }

        public string CostCode { get; set; }

        public decimal QtyLeft { get; set; }

        public ProductPriceDto Pricing { get; set; }

        public bool IsPromotionItem { get; set; }

        public bool IsDiscounted { get; set; }

        public bool IsFixedConfiguration { get; set; }

        public bool QuoteRequired { get; set; }

        public IList<BreakPriceDto> BreakPrices { get; set; }

        public AvailabilityDto Availability { get; set; }

        public decimal QtyOnHand { get; set; }

        public bool CanAddToCart { get; set; }

        public bool IsQtyAdjusted { get; set; }

        public bool HasInsufficientInventory { get; set; }

        /// <summary>Gets or sets the ability to order with no inventory.</summary>
        public bool CanBackOrder { get; set; }

        public string SalePriceLabel { get; set; }

        public bool IsSubscription { get; set; }

        public ProductSubscriptionDto ProductSubscription { get; set; }

        public bool IsRestricted { get; set; }

        public bool IsActive { get; set; }

        public Brand Brand { get; set; }

        [JsonIgnore]
        public bool HasLineNotes => !string.IsNullOrWhiteSpace(Notes);

        [JsonIgnore]
        public string Status;
    }

    public class SectionOptionDto
    {
        public Guid SectionOptionId { get; set; }

        public string SectionName { get; set; }

        public string OptionName { get; set; }
    }

    public class ProductSubscriptionDto
    {
        public bool SubscriptionAddToInitialOrder { get; set; }

        public bool SubscriptionAllMonths { get; set; }

        public bool SubscriptionApril { get; set; } = true;

        public bool SubscriptionAugust { get; set; } = true;

        public string SubscriptionCyclePeriod { get; set; } = string.Empty;

        public bool SubscriptionDecember { get; set; } = true;

        public bool SubscriptionFebruary { get; set; } = true;

        public bool SubscriptionFixedPrice { get; set; }

        public bool SubscriptionJanuary { get; set; } = true;

        public bool SubscriptionJuly { get; set; } = true;

        public bool SubscriptionJune { get; set; } = true;

        public bool SubscriptionMarch { get; set; } = true;

        public bool SubscriptionMay { get; set; } = true;

        public bool SubscriptionNovember { get; set; } = true;

        public bool SubscriptionOctober { get; set; } = true;

        public int SubscriptionPeriodsPerCycle { get; set; }

        public bool SubscriptionSeptember { get; set; } = true;

        public Guid? SubscriptionShipViaId { get; set; }

        public int SubscriptionTotalCycles { get; set; }
    }
}