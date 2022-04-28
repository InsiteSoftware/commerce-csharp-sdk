using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetBillTosResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<BillTo> BillTos { get; set; }
    }
}
