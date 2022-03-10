using CommerceApiSDK.Attributes;
using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class ShipTosQueryParameters : BaseQueryParameters
    {
        public override int? Page { get; set; } = 1;

        public override int? PageSize { get; set; } = 16;

        public Guid BillToId { get; set; }

        public string Filter { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Exclude { get; set; }

        /// <summary>Options: Approvals, AssignedOnly, ExcludeBillTo, ExcludeCreateNew, ExcludeShowAll, Validation</summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
