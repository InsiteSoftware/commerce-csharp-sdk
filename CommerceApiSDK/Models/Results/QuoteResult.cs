using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class QuoteResult : BaseModel
    {
        public IList<QuoteDto> Quotes { get; set; }
        public IList<SalespersonListDto> SalespersonList { get; set; }

        public Pagination Pagination { get; set; }
    }
}
