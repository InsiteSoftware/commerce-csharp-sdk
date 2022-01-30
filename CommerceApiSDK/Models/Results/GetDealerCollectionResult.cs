namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetDealerCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Dealer> Dealers { get; set; }

        public double DefaultLatitude { get; set; }

        public double DefaultLongitude { get; set; }

        public double DefaultRadius { get; set; }

        public string DistanceUnitOfMeasure { get; set; }
    }
}