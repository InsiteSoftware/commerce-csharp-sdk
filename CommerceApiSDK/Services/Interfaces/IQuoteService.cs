namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;

    public interface IQuoteService
    {
        Task<QuoteResult> GetQuotes(QuoteQueryParameters quoteQueryParameters);

        Task<QuoteDto> GetQuote(string quoteId);

        Task<QuoteDto> SaveQuote(QuoteDto quote);

        Task<QuoteDto> RequestQuote(RequesteAQuoteParameters param);

        Task<QuoteDto> RequestQuote(SalesRepRequesteAQuoteParameters param);

        Task<bool> DeleteQuote(string quoteId);

        Task<QuoteDto> SubmitQuote(QuoteDto quote);

        Task<QuoteMessage> PostQuoteMessage(string quoteId, QuoteMessage message);

        Task<ServiceResponse<QuoteDto>> QuoteAll(QuoteAllQueryParameters param);

        Task<QuoteLine> PatchQuoteLine(string quoteId, QuoteLine quoteLine);

        Task<QuoteDto> QuoteLinePricing(string quoteId, QuoteLinePricingQueryParameters param);
    }
}
