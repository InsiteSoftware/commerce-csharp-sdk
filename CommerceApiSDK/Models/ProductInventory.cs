using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class ProductInventory
    {
        public Guid ProductId { get; set; }

        public decimal QtyOnHand { get; set; }

        public List<InventoryAvailability> InventoryAvailabilityDtos { get; set; }

        public List<InventoryWarehouses> InventoryWarehousesDtos { get; set; }

        public Dictionary<string, string> AdditionalResults { get; set; }
    }

}

