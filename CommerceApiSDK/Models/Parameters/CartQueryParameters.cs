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

        /// <summary>
        /// Here are parameters to be passed in the Expand List.
        /// Options: cartlines, costcodes, shipping, tax, carriers, paymentoptions
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
