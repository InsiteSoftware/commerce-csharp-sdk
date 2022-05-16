using System;

namespace CommerceApiSDK.Models
{
    public class StyleValue
    {
        public string StyleTraitName { get; set; }

        public Guid StyleTraitId { get; set; }

        public Guid StyleTraitValueId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }

        public int SortOrder { get; set; }

        public bool IsDefault { get; set; }

        // for V2
        public Guid Id { get; set; }
    }
}
