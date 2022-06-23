using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class Rma : BaseModel
    {
        public string OrderNumber { get; set; }

        public string Notes { get; set; }

        public string Message { get; set; }

        public IList<RmaLine> RmaLines { get; set; }
    }

    public class RmaLine
    {
        public decimal Line { get; set; }

        public int RmaQtyRequested { get; set; }

        public string RmaReasonCode { get; set; }
    }
}

