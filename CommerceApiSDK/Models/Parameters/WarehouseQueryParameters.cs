using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WarehouseQueryParameters : BaseQueryParameters
    {
        public double Latitude { get; set; } = 0;

        public double Longitude { get; set; } = 0;

        public bool OnlyPickupWarehouses { get; set; } = true;

        /// <summary>
        /// Here are parameters to be passed in the Expand List.
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;

        public bool UseDefaultLocation { get; set; }

        public int Radius { get; set; }

        public bool ExcludeCurrentPickupWarehouse { get; set; }
    }
}
