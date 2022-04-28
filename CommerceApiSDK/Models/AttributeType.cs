using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class AttributeType
    {
        public Guid AttributeTypeId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public int Sort { get; set; }

        public IList<AttributeValue> AttributeValueFacets { get; set; }

        // for V2
        public Guid Id { get; set; }

        public string Label { get; set; }

        public bool IsFilter { get; set; }

        public bool IsComparable { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public IList<AttributeValue> AttributeValues { get; set; }
    }
}

