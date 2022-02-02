using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class QuoteAllQueryParameters
    {
        public string CalculationMethod { get; set; }

        public int Percent { get; set; }

        public Guid QuoteId { get; set; }
    }
}
