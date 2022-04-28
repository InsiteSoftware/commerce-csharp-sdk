using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class CatalogPage : BaseModel
    {
        public Category Category { get; set; }

        public Guid? BrandId { get; set; }

        public Guid? ProductLineId { get; set; }

        public Guid? ProductId { get; set; }

        public string ProductName { get; set; }

        public string Title { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string CanonicalPath { get; set; }

        public Dictionary<string, string> AlternateLanguageUrls { get; set; }

        public bool IsReplacementProduct { get; set; }

        public List<BreadCrumb> BreadCrumbs { get; set; }

        public bool ObsoletePath { get; set; }

        public bool NeedRedirect { get; set; }

        public string RedirectUrl { get; set; }

        public string PrimaryImagePath { get; set; }

        public string OpenGraphTitle { get; set; }

        public string OpenGraphImage { get; set; }

        public string OpenGraphUrl { get; set; }
    }
}
