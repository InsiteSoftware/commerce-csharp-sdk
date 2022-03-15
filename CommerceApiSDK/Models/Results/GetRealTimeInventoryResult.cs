using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetRealTimeInventoryResult : BaseModel
    {
        public List<ProductInventoryDto> RealTimeInventoryResults { get; set; }
    }
}
