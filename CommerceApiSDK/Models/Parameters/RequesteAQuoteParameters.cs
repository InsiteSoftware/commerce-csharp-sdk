namespace CommerceApiSDK.Models.Parameters
{
    public class SalesRepRequesteAQuoteParameters : RequesteAQuoteParameters
    {
        public string UserId { get; set; }
    }

    public class RequesteAQuoteParameters
    {
        public string QuoteId { get; set; }

        public string JobName { get; set; }

        public string Note { get; set; }

        public bool IsJobQuote { get; set; }
    }
}
