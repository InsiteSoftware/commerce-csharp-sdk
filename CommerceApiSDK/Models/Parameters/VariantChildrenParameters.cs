using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class VariantChildrenParameters : BaseQueryParameters
    {
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public string IncludeAttributes { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; }
    }
}

