using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    /// <summary>
    /// Commerce API Category model.
    /// </summary>
    public class Category : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string UrlSegment { get; set; }

        public string SmallImagePath { get; set; }

        public string LargeImagePath { get; set; }

        public string ImageAltText { get; set; }

        public DateTime ActivateOn { get; set; }

        public DateTime? DeactivateOn { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string HtmlContent { get; set; }

        public int SortOrder { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsDynamic { get; set; }

        public IList<Category> SubCategories { get; set; }

        public string Path { get; set; }

        /// <summary>
        /// Hero image on mobile category pages
        /// </summary>
        public string MobileBannerImageUrl { get; set; }

        /// <summary>
        /// Large text on mobile category pages
        /// </summary>
        public string MobilePrimaryText { get; set; }

        /// <summary>
        /// Small text on mobile category pages
        /// </summary>
        public string MobileSecondaryText { get; set; }

        public string MobileTextJustification { get; set; }

        public string MobileTextColor { get; set; }
    }
}
