using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetRealTimePricingResult : BaseModel
    {
        public IList<ProductPrice> RealTimePricingResults { get; set; }
    }
}
