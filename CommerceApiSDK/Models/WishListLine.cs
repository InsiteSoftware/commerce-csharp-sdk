using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WishListLine : BaseModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string SmallImagePath { get; set; }

        public string ManufacturerItem { get; set; }

        public string CustomerName { get; set; }

        public string ShortDescription { get; set; }

        public decimal QtyOrdered { get; set; }

        public string ERPNumber { get; set; }

        public ProductPrice Pricing { get; set; }

        public bool IsActive { get; set; }

        public bool CanEnterQuantity { get; set; }

        public bool CanAddToCart { get; set; }

        public Availability Availability { get; set; }

        public string UnitOfMeasure { get; set; }

        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        public string Notes { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDiscontinued { get; set; }

        public bool TrackInventory { get; set; }

        public int SortOrder { get; set; }

        public Brand Brand { get; set; }

        public bool QuoteRequired { get; set; }
    }
}
