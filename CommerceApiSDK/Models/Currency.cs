using System;

namespace CommerceApiSDK.Models
{
    public class Currency : BaseModel
    {
        public Guid ID { get; set; }

        public string CurrencyCode { get; set; }

        public string Description { get; set; }

        public string CurrencySymbol { get; set; }

        public bool IsDefault { get; set; }
    }
}

