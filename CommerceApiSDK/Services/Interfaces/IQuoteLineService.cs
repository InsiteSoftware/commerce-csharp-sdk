namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;

    public interface IQuoteLineService
    {
        Task<QuoteLine> GetQuoteLine(string quoteId, string quoteLineId);

        Task<QuoteLine> UpdateQuoteLine(string quoteId, QuoteLine quoteLine);
    }
}
