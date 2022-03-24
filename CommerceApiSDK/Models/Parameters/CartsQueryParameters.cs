using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class CartsQueryParameters : BaseQueryParameters
    {
        public Guid? BillToId { get; set; }

        public Guid? ShipToId { get; set; }

        public string Status { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string OrderTotalOperator { get; set; }

        public decimal OrderTotal { get; set; }

        public string OrderSubtotalOperator { get; set; }

        public decimal OrerSubtotal { get; set; }

        public Guid? VmiLocationId { get; set; }
    }
}
