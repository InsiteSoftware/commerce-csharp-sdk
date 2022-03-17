using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WarehouseQueryParameters : BaseQueryParameters
    {
        public double latitude { get; set; } = 0;

        public double longitude { get; set; } = 0;

        public int pageNumber { get; set; } = 1;

        public int pageSize { get; set; } = 16;

        public string sort { get; set; } = "Distance";

        public bool onlyPickupWarehouses { get; set; } = true;
    }
}
