using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WarehouseQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public double latitude { get; set; } = 0;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public double longitude { get; set; } = 0;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int pageNumber { get; set; } = 1;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int pageSize { get; set; } = 16;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string sort { get; set; } = "Distance";

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public bool onlyPickupWarehouses { get; set; } = true;
    }
}
