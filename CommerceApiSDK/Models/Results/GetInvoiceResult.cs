namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetInvoiceResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Invoice> Invoices { get; set; }
    }
}