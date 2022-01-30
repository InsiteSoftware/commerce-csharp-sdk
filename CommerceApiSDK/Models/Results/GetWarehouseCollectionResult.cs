namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetWarehouseCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Warehouse> Warehouses { get; set; }
    }
}