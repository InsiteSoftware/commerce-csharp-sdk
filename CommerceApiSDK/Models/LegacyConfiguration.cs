using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class LegacyConfiguration
    {
        public IList<ConfigSection> Sections { get; set; }

        public bool HasDefaults { get; set; }

        public bool IsKit { get; set; }
    }

    public class ConfigSection
    {
        public string SectionName { get; set; }

        public IList<ConfigSectionOption> Options { get; set; }

        // for V2
        public Guid Id { get; set; }

        public int SortOrder { get; set; }
    }

    public class ConfigSectionOption
    {
        public Guid SectionOptionId { get; set; }

        public string SectionName { get; set; }

        public string ProductName { get; set; }

        public Guid? ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool UserProductPrice { get; set; }

        public bool Selected { get; set; }

        public int SortOrder { get; set; }

        // for V2
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }
    }
}
