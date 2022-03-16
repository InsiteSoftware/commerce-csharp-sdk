using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
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

    public class CurrentCartQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public bool getCartlines { get; set; } = false;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool getCostCodes { get; set; } = false;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool getShipping { get; set; } = false;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool getTax { get; set; } = false;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool getCarriers { get; set; } = false;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool getPaymentMethods { get; set; } = false;
    }

}
