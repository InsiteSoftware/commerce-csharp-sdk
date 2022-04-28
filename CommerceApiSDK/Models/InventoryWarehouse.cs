using System;
namespace CommerceApiSDK.Models
{
    public class InventoryWarehouse : Availability
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Qty { get; set; }
    }
}
