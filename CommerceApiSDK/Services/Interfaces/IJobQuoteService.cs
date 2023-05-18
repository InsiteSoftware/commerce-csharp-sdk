using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IJobQuoteService
    {
        Task<ServiceResponse<JobQuoteResult>> GetJobQuotes();

        Task<ServiceResponse<JobQuoteDto>> GetJobQuote(string jobQuoteId);

        Task<ServiceResponse<JobQuoteDto>> UpdateJobQuote(JobQuoteUpdateParameter jobQuoteUpdate);
    }
}
