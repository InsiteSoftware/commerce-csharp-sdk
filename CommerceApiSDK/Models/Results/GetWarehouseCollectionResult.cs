using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetWarehouseCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Warehouse> Warehouses { get; set; }

        public string DistanceUnitOfMeasure { get; set; }

        public double DefaultLatitude { get; set; }

        public double DefaultLongitude { get; set; }

        public int DefaultRadius { get; set; }
    }
}
