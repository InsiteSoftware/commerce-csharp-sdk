namespace CommerceApiSDK.Models
{
    using System;
    using System.Collections.Generic;

    public class JobQuoteDto : Cart
    {
        public string JobQuoteId { get; set; }

        public bool IsEditable { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string JobName { get; set; }

        public ICollection<JobQuoteLine> JobQuoteLineCollection { get; set; }

        public string CustomerName { get; set; }

        public string ShipToFullAddress { get; set; }

        public decimal OrderTotal { get; set; }

        public string OrderTotalDisplay { get; set; }
    }

    public class JobQuoteLine : CartLine
    {
        public PricingRfq PricingRfq { get; set; }

        /// <summary>Gets or sets the max quantity.</summary>
        public decimal MaxQty { get; set; }

        /// <summary>Gets or sets the quantity sold.</summary>
        public decimal QtySold { get; set; }

        /// <summary>Gets or sets the quantity order.</summary>
        public decimal QtyRequested { get; set; }
    }
}