using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class RealTimeInventoryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public List<Guid> ProductIds { get; set; }

        public bool IncludeAlternateInventory { get; set; }

        [QueryParameter(QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; } = null;
    }
}
