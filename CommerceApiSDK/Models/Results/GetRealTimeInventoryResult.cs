namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetRealTimeInventoryResult : BaseModel
    {
        public List<ProductInventoryDto> RealTimeInventoryResults { get; set; }
    }
}
