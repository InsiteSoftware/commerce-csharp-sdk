using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetOrderApprovalCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Cart> cartCollection { get; set; }

    }
}
