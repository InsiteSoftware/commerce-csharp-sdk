using System;

namespace CommerceApiSDK.Models
{
    public class Specification
    {
        public Guid SpecificationId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public double SortOrder { get; set; }

        public bool IsActive { get; set; }

        public Specification ParentSpecification { get; set; }

        public string HtmlContent { get; set; }

        public Specification Specifications { get; set; }
    }
}
