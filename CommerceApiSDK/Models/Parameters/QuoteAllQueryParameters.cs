namespace CommerceApiSDK.Models.Parameters
{
    using System;
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class QuoteAllQueryParameters
    {
        public string CalculationMethod { get; set; }

        public int Percent { get; set; }

        public Guid QuoteId { get; set; }
    }
}
