namespace CommerceApiSDK.Models.Parameters
{
    using System;
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class CartQueryParameters : BaseQueryParameters
    {
        public string Status { get; set; }
    }

    public class CartDetailQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid CartId { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
