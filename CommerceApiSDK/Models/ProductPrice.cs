using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class ProductPrice : BaseModel
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

        /// <summary>Gets or sets the Quantity UnitNetPrice</summary>
        public decimal ExtendedUnitNetPrice { get; set; }

        /// <summary>Gets or sets the Formatted Quantity UnitNetPrice</summary>
        public string ExtendedUnitNetPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        public string UnitOfMeasure { get; set; }

        public decimal VatRate { get; set; }

        public decimal VatAmount { get; set; }

        public string VatAmountDisplay { get; set; }

        public IList<BreakPriceDto> UnitListBreakPrices { get; set; }

        public IList<BreakPriceDto> UnitRegularBreakPrices { get; set; }

        public decimal RegularPrice { get; set; }

        public string RegularPriceDisplay { get; set; }

        public decimal ExtendedRegularPrice { get; set; }

        public string ExtendedRegularPriceDisplay { get; set; }

        public decimal ActualPrice { get; set; }

        public string ActualPriceDisplay { get; set; }

        public decimal ExtendedActualPrice { get; set; }

        public string ExtendedActualPriceDisplay { get; set; }

        public IList<BreakPriceDto> RegularBreakPrices { get; set; }

        public IList<BreakPriceDto> ActualBreakPrices { get; set; }
    }
}
