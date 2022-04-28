namespace CommerceApiSDK.Models.Parameters
{
    public class DealerLocationFinderQueryParameters : BaseQueryParameters
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Radius { get; set; }
    }
}
