using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class JobQuoteResult : BaseModel
    {
        public IList<JobQuoteDto> JobQuotes { get; set; }

        public Pagination Pagination { get; set; }
    }
}
