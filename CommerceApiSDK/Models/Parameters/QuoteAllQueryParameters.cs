namespace CommerceApiSDK.Models.Parameters
{
    using System;

    public class QuoteAllQueryParameters
    {
        public string CalculationMethod { get; set; }

        public int Percent { get; set; }

        public Guid QuoteId { get; set; }
    }
}
