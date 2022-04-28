using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetRealTimeInventoryResult : BaseModel
    {
        public List<ProductInventory> RealTimeInventoryResults { get; set; }
    }
}
