using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetOrderStatusMappingsResult : BaseModel
    {
        public List<OrderStatusMapping> OrderStatusMappings { get; set; }
    }
}
