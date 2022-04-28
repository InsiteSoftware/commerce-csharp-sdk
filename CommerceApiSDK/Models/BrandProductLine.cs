using System;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class BrandProductLine : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long SortOrder { get; set; }

        public string ProductListPagePath { get; set; }

        public string FeaturedImagePath { get; set; }

        public string FeaturedImageAltText { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsSponsored { get; set; }

        [JsonIgnore]
        public bool IsLoading { get; set; }
    }
}
