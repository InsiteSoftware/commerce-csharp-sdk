using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IJobQuoteService
    {
        Task<JobQuoteResult> GetJobQuotes();

        Task<JobQuoteDto> GetJobQuote(string jobQuoteId);

        Task<JobQuoteDto> UpdateJobQuote(JobQuoteUpdateParameter jobQuoteUpdate);
    }
}
