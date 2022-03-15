using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class ProductPriceQueryParameter
    {
        public Guid ProductId { get; set; }

        public string UnitOfMeasure { get; set; }

        public decimal QtyOrdered { get; set; }

        public List<Guid> Configuration { get; set; }
    }
}
