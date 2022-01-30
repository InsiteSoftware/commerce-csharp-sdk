using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class QuoteRequestedParameter
    {
        public string QuoteId { get; set; }

        public string Status { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
