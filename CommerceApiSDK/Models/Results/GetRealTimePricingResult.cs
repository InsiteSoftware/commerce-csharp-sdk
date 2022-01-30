namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetRealTimePricingResult : BaseModel
    {
        public IList<ProductPriceDto> RealTimePricingResults { get; set; }
    }
}
