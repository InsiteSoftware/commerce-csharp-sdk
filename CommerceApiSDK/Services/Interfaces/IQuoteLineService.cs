using System.Threading.Tasks;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IQuoteLineService
    {
        Task<QuoteLine> GetQuoteLine(string quoteId, string quoteLineId);

        Task<QuoteLine> UpdateQuoteLine(string quoteId, QuoteLine quoteLine);
    }
}
