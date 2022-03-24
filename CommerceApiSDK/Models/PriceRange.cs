using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class PriceRange
    {
        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }

        public int Count { get; set; }

        public IList<PriceFacet> PriceFacets { get; set; }
    }
}

