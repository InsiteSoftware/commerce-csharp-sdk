using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class CatalogpagesResult : BaseModel
    {
        public Category category { get; set; }
        public string brandId { get; set; }
        public string productLineId { get; set; }
        public string productId { get; set; }
        public string productName { get; set; } 
        public string title { get; set; }
        public string metaDescription { get; set; }
        public string metaKeywords { get; set; }
        public string canonicalPath { get; set; }
        public object alternateLanguageUrls { get; set; }
        public bool isReplacementProduct { get; set; }
        public List<BreadCrumb> breadCrumbs { get; set; }
        public bool obsoletePath { get; set; }
        public bool needRedirect { get; set; }
        public string redirectUrl { get; set; }
        public string primaryImagePath { get; set; }
        public string openGraphTitle { get; set; }
        public string openGraphImage { get; set; }
        public string openGraphUrl { get; set; }
    }

    public class BreadCrumb
    {
        public string text { get; set; }
        public string url { get; set; }
        public string categoryId { get; set; }
    }
    
}
