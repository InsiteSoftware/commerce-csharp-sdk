using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetInvoiceResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Invoice> Invoices { get; set; }
    }
}