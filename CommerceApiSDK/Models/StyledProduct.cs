using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
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
        public ProductPrice Pricing { get; set; }

        /// <summary>Gets or sets a value indicating whether [quote required].</summary>
        public bool QuoteRequired { get; set; }

        /// <summary>Gets or sets the style values.</summary>
        public IList<StyleValue> StyleValues { get; set; }

        /// <summary>Gets or sets the availability.</summary>
        public Availability Availability { get; set; }

        /// <summary>Gets or sets the list of all available units of measure.</summary>
        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        /// <summary>Gets or sets the list of all product images.</summary>
        public IList<ProductImage> ProductImages { get; set; }

        /// <summary>Gets or sets the warehouses.</summary>
        public IList<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        public bool TrackInventory { get; set; }
    }
}

