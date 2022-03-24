using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class InventoryWarehouses
    {
        public string UnitOfMeasure { get; set; }

        public List<Warehouse> WarehouseDtos { get; set; }
    }
}

