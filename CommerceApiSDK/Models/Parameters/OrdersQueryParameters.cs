using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class OrdersQueryParameters : BaseQueryParameters
    {
        public string OrderNumber { get; set; }

        public string PoNumber { get; set; }

        public string Search { get; set; }

        public List<string> Status { get; set; } = null;

        public string CustomerSequence { get; set; } = "-1"; // Show All

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string OrderTotalOperator { get; set; }

        public decimal OrderTotal { get; set; }

        public string ProductErpNumber { get; set; }

        public bool VmiOrdersOnly { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid? VmiLocationId { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}