using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class ProductPriceQueryParameters : BaseQueryParameters
    {
        public Guid ProductId { get; set; }

        public decimal QtyOrdered { get; set; }

        public string UnitOfMeasure { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<Guid> Configuration { get; set; } = null;

        public object Properties { get; set; }

        public bool IsValid { get; set; }

        public string ValidationMessage { get; set; }
    }
}
