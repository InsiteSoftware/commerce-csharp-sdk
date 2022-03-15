using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetRealTimePricingResult : BaseModel
    {
        public IList<ProductPriceDto> RealTimePricingResults { get; set; }
    }
}
