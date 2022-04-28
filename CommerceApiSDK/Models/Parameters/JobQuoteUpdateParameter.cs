using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class JobQuoteLineUpdate
    {
        public Guid Id { get; set; }
        public decimal? QtyOrdered { get; set; }
    }

    public class JobQuoteUpdateParameter
    {
        public string JobQuoteId { get; set; }

        public IList<JobQuoteLineUpdate> JobQuoteLineCollection { get; set; }
    }
}
