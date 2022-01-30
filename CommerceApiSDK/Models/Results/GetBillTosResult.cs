namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetBillTosResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<BillTo> BillTos { get; set; }
    }
}
