namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetOrderStatusMappingsResult : BaseModel
    {
        public List<OrderStatusMapping> OrderStatusMappings { get; set; }
    }
}
