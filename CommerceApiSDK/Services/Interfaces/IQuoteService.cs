using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<ServiceResponse<QuoteResult>> GetQuotes(QuoteQueryParameters quoteQueryParameters);

        Task<ServiceResponse<QuoteDto>> GetQuote(string quoteId);

        Task<ServiceResponse<QuoteDto>> SaveQuote(QuoteDto quote);

        Task<ServiceResponse<QuoteDto>> RequestQuote(RequesteAQuoteParameters param);

        Task<ServiceResponse<QuoteDto>> RequestQuote(SalesRepRequesteAQuoteParameters param);

        Task<bool> DeleteQuote(string quoteId);

        Task<ServiceResponse<QuoteDto>> SubmitQuote(QuoteDto quote);

        Task<ServiceResponse<QuoteMessage>> PostQuoteMessage(string quoteId, QuoteMessage message);

        Task<ServiceResponse<QuoteDto>> QuoteAll(QuoteAllQueryParameters param);

        Task<ServiceResponse<QuoteLine>> PatchQuoteLine(string quoteId, QuoteLine quoteLine);

        Task<ServiceResponse<QuoteDto>> QuoteLinePricing(string quoteId, QuoteLinePricingQueryParameters param);

        Task<ServiceResponse<QuoteLine>> GetQuoteLine(string quoteId, string quoteLineId);

        Task<ServiceResponse<QuoteLine>> UpdateQuoteLine(string quoteId, QuoteLine quoteLine);
    }
}
