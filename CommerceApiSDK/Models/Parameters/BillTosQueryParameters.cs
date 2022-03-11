using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class BillTosQueryParameters : BaseQueryParameters
    {
        public override int? Page { get; set; } = 1;

        public override int? PageSize { get; set; } = 16;

        public string Filter { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Exclude { get; set; }

        /// <summary>Options: ShipTos, Validation</summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
