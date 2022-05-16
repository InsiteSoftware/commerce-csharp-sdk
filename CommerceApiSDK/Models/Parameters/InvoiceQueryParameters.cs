using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class InvoiceQueryParameters : BaseQueryParameters
    {
        /// <summary>Gets or sets a value indicating whether [show open only].</summary>
        public bool ShowOpenOnly { get; set; }

        public string InvoiceNumber { get; set; }

        public string OrderNumber { get; set; }

        /// <summary>Gets or sets the customer po.</summary>
        public string PoNumber { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string CustomerSequence { get; set; } = "-1"; // Show All

        [QueryParameter(QueryOptions.DoNotQuery)]
        public ShipTo ShipTo { get; set; }
    }

    public class InvoiceDetailParameter : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public string InvoiceNumber { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }

    public class InvoiceEmailParameter : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public string EmailTo { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string EmailFrom { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Subject { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Message { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string EntityId { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string EntityName { get; set; } = null;
    }
}
