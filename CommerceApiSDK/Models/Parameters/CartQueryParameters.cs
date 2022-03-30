using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
	public class CartQueryParameters : BaseQueryParameters
	{
        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid? CartId { get; set; }

        public int AlsoPurchasedMaxResults { get; set; }

        public bool ForceRecalculation { get; set; }

        public bool AllowInvalidAddress { get; set; }

        /// <summary>
        /// Here are parameters to be passed in the Expand List.
        /// Options: cartlines, costcodes, shipping, tax, carriers, paymentoptions,
        /// shiptos, validation, restrictions, creditcardbillingaddress, warehouses,
        /// alsopurchased, hiddenproducts, paymentoptions
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}

