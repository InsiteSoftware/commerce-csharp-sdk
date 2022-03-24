using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class ProductPriceQueryParameter
    {
        public Guid ProductId { get; set; }

        public string UnitOfMeasure { get; set; }

        public decimal? QtyOrdered { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<Guid> Configuration { get; set; }
    }
}
