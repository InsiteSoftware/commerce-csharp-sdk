namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class JobQuoteResult : BaseModel
    {
        public IList<JobQuoteDto> JobQuotes { get; set; }

        public Pagination Pagination { get; set; }
    }
}
