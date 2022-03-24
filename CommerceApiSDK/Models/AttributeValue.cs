using System;

namespace CommerceApiSDK.Models
{
    public class AttributeValue
    {
        public Guid AttributeValueId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }

        // for V2
        public Guid Id { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }
}

