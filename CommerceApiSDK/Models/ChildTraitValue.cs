using System;

namespace CommerceApiSDK.Models
{
    public class ChildTraitValue
    {
        public Guid Id { get; set; }

        public Guid StyleTraitId { get; set; }

        public string Value { get; set; }

        public string ValueDisplay { get; set; }
    }
}
