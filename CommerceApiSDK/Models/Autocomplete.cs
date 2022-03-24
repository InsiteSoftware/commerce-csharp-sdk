using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class Autocomplete : BaseModel
    {
        public IList<AutocompleteProduct> Products { get; set; }
    }

    public class AutocompleteProduct : BaseModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string ErpNumber { get; set; }

        public string Url { get; set; }

        public string ManufacturerItemNumber { get; set; }

        public bool IsNameCustomerOverride { get; set; }

        public string BrandName { get; set; }

        public string BrandDetailPagePath { get; set; }

        public string BinNumber { get; set; }
    }

    public class AutocompleteBrand : BaseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public Guid? ProductLineId { get; set; }

        public string ProductLineName { get; set; }
    }

    public class AutocompleteCategory : BaseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }
    }
}
