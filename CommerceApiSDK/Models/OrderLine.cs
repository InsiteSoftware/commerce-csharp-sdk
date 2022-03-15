using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class OrderLine : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the product identifier.</summary>
        public string ProductId { get; set; }

        /// <summary>Gets or sets the product URI.</summary>
        public string ProductUri { get; set; }

        /// <summary>Gets or sets the medium image path.</summary>
        public string MediumImagePath { get; set; }

        /// <summary>Gets or sets the alt text.</summary>
        public string AltText { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>
        public string ProductName { get; set; }

        /// <summary>Gets or sets the manufacturer item.</summary>
        public string ManufacturerItem { get; set; }

        /// <summary>Gets or sets the name of the customer.</summary>
        public string CustomerName { get; set; }

        /// <summary>Gets or sets the short description.</summary>
        public string ShortDescription { get; set; }

        /// <summary>Gets or sets the product erp number.</summary>
        public string ProductErpNumber { get; set; }

        /// <summary>Gets or sets the customer product number.</summary>
        public string CustomerProductNumber { get; set; }

        /// <summary>Gets or sets the required date.</summary>
        public DateTime? RequiredDate { get; set; }

        /// <summary>Gets or sets the last ship date.</summary>
        public DateTime? LastShipDate { get; set; }

        /// <summary>Gets or sets the customer number.</summary>
        public string CustomerNumber { get; set; }

        /// <summary>Gets or sets the customer sequence.</summary>
        public string CustomerSequence { get; set; }

        /// <summary>Gets or sets the type of the line.</summary>
        public string LineType { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public string Status { get; set; }

        /// <summary>Gets or sets the line number.</summary>
        public decimal LineNumber { get; set; }

        /// <summary>Gets or sets the release number.</summary>
        public decimal ReleaseNumber { get; set; }

        /// <summary>Gets or sets the line po reference.</summary>
        public string LinePOReference { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the warehouse.</summary>
        public string Warehouse { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        public string Notes { get; set; }

        /// <summary>Gets or sets the qty ordered.</summary>
        public decimal QtyOrdered { get; set; }

        /// <summary>Gets or sets the qty shipped.</summary>
        public decimal QtyShipped { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the unit of measure display.</summary>
        public string UnitOfMeasureDisplay { get; set; }

        /// <summary>Gets or sets the unit of measure description.</summary>
        public string UnitOfMeasureDescription { get; set; }

        /// <summary>Gets or sets the inventory availability information.</summary>
        public AvailabilityDto Availability { get; set; }

        /// <summary>Gets or sets the inventory qty ordered.</summary>
        public decimal InventoryQtyOrdered { get; set; }

        /// <summary>Gets or sets the inventory qty shipped.</summary>
        public decimal InventoryQtyShipped { get; set; }

        /// <summary>Gets or sets the unit net price.</summary>
        public decimal UnitNetPrice { get; set; }

        /// <summary>Gets or sets the Quantity UnitNetPrice</summary>
        public decimal ExtendedUnitNetPrice { get; set; }

        /// <summary>Gets or sets the discount percent.</summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>Gets or sets the unit discount amount.</summary>
        public decimal UnitDiscountAmount { get; set; }

        /// <summary>Gets or sets the total discount amount.</summary>
        public decimal TotalDiscountAmount { get; set; }

        /// <summary>Gets or sets the total regular price.</summary>
        public decimal TotalRegularPrice { get; set; }

        /// <summary>Gets or sets the unit list price.</summary>
        public decimal UnitListPrice { get; set; }

        /// <summary>Gets or sets the unit regular price.</summary>
        public decimal UnitRegularPrice { get; set; }

        /// <summary>Gets or sets the unit cost.</summary>
        public decimal UnitCost { get; set; }

        /// <summary>Gets or sets the other charges.</summary>
        public decimal OrderLineOtherCharges { get; set; }

        /// <summary>Gets or sets the return reason.</summary>
        public string ReturnReason { get; set; }

        /// <summary>Gets or sets the return qty requested.</summary>
        public decimal RmaQtyRequested { get; set; }

        /// <summary>Gets or sets the return qty received.</summary>
        public decimal RmaQtyReceived { get; set; }

        /// <summary>Gets or sets the unit net price display.</summary>
        public string UnitNetPriceDisplay { get; set; }

        /// <summary>Gets or sets the Formatted Quantity UnitNetPrice</summary>
        public string ExtendedUnitNetPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit discount amount display.</summary>
        public string UnitDiscountAmountDisplay { get; set; }

        /// <summary>Gets or sets the total discount amount display.</summary>
        public string TotalDiscountAmountDisplay { get; set; }

        /// <summary>Gets or sets the total regular price display.</summary>
        public string TotalRegularPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit list price display.</summary>
        public string UnitListPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit regular price display.</summary>
        public string UnitRegularPriceDisplay { get; set; }

        /// <summary>Gets or sets the unit cost display.</summary>
        public string UnitCostDisplay { get; set; }

        /// <summary>Gets or sets the other charges display.</summary>
        public string OrderLineOtherChargesDisplay { get; set; }

        /// <summary>Gets or sets the cost codes.</summary>
        public string CostCode { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can add to cart.</summary>
        public bool CanAddToCart { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is active product.</summary>
        public bool IsActiveProduct { get; set; }

        /// <summary>Gets or sets the section options.</summary>
        public IList<SectionOptionDto> SectionOptions { get; set; }

        /// <summary>Gets or sets the sale price label.</summary>
        public string SalePriceLabel { get; set; }

        public Brand Brand { get; set; }
    }
}
