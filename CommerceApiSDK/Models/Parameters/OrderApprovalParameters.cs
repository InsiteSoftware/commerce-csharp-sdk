using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class OrderApprovalParameters : BaseQueryParameters
    {
        public string ShipToId { get; set; }

        public string OrderNumber { get; set; }

        public string OrderTotal { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public IList<string> OrderTotalOperator { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public ShipTo ShipTo { get; set; }
    }
}
