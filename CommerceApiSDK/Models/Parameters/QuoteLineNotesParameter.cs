namespace CommerceApiSDK.Models.Parameters
{
    public class QuoteLineNotesParameter
    {
        public string QuoteId { get; set; }

        public bool IsCanEdit { get; set; }

        public QuoteLine QuoteLine { get; set; }
    }
}
