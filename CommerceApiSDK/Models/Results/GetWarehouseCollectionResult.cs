using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetWarehouseCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Warehouse> Warehouses { get; set; }
    }
}