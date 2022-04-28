using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WarehousesQueryParameters : BaseQueryParameters
    {
        public double Latitude { get; set; } = 0;

        public double Longitude { get; set; } = 0;

        public bool OnlyPickupWarehouses { get; set; } = true;

        public bool UseDefaultLocation { get; set; }

        public int Radius { get; set; }

        public bool ExcludeCurrentPickupWarehouse { get; set; }

        /// <summary>
        /// Options: properties
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
