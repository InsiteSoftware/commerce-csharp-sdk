using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;

namespace CommerceApiSDK.Models.Parameters
{
    public class QuoteQueryParameters : BaseQueryParameters
    {
        public string UserId { get; set; }

        public string SalesRepNumber { get; set; }

        public string CustomerId { get; set; }

        public IList<string> Statuses { get; set; }

        public string QuoteNumber { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? ExpireFromDate { get; set; }

        public DateTime? ExpireToDate { get; set; }

        public IList<string> Types { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;

        [QueryParameter(QueryOptions.DoNotQuery)]
        public BillTo BillTo { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public CatalogTypeDto SelectedUser { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public CatalogTypeDto SelectedSalesRep { get; set; }
    }
}
