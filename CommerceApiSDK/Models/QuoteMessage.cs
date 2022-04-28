using System;

namespace CommerceApiSDK.Models
{
    public class QuoteMessage : BaseModel
    {
        public DateTime CreatedDate { get; set; }

        public string QuoteId { get; set; }

        public string Message { get; set; }

        public string DisplayName { get; set; }

        public string Body { get; set; }
    }
}
