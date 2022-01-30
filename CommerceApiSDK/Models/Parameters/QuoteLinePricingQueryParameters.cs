namespace CommerceApiSDK.Models.Parameters
{
    using System;
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class QuoteLinePricingQueryParameters
    {
        public Guid Id { get; set; }

        public PricingRfq PricingRfq { get; set; }
    }
}
