using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WishListLine : BaseModel
    {
        public Guid Id { get; set; }

        public string ProductUri { get; set; }

        public Guid ProductId { get; set; }

        public string SmallImagePath { get; set; }

        public string AltText { get; set; }

        public string ProductName { get; set; }

        public string ManufacturerItem { get; set; }

        public string CustomerName { get; set; }

        public string ShortDescription { get; set; }

        public decimal QtyOnHand { get; set; }

        public decimal QtyOrdered { get; set; }

        public string ERPNumber { get; set; }

        public ProductPrice Pricing { get; set; }

        public bool QuoteRequired { get; set; }

        public bool IsActive { get; set; }

        public bool CanEnterQuantity { get; set; }

        public bool CanShowPrice { get; set; }

        public bool CanAddToCart { get; set; }

        public bool CanShowUnitOfMeasure { get; set; }

        public bool CanBackOrder { get; set; }

        public bool TrackInventory { get; set; }

        public Availability Availability { get; set; }

        public IList<BreakPriceDto> BreakPrices { get; set; }

        public string UnitOfMeasure { get; set; }

        public string unitOfMeasureDisplay { get; set; }

        public string unitOfMeasureDescription { get; set; }

        public string BaseUnitOfMeasure { get; set; }

        public string BaseUnitOfMeasureDisplay { get; set; }

        public decimal QtyPerBaseUnitOfMeasure { get; set; }

        public string SelectedUnitOfMeasure { get; set; }

        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        public string PackDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Notes { get; set; }

        public string CreatedByDisplayName { get; set; }

        public bool IsSharedLine { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDiscontinued { get; set; }

        public int SortOrder { get; set; }

        public Brand Brand { get; set; }

        public bool IsQtyAdjusted { get; set; }

        public bool AllowZeroPricing { get; set; }
    }
}
