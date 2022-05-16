using System;

namespace CommerceApiSDK.Models
{
    public class ProductUnitOfMeasure : BaseModel
    {
        public Guid ProductUnitOfMeasureId { get; set; }

        public string UnitOfMeasure { get; set; }

        public string UnitOfMeasureDisplay { get; set; }

        public string Description { get; set; }

        public double QtyPerBaseUnitOfMeasure { get; set; }

        public string RoundingRule { get; set; }

        public bool IsDefault { get; set; }

        public Availability Availability { get; set; }
    }
}
