using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetBrandSubCategoriesResult : BaseModel
    {
        public string BrandId { get; set; }

        public string CategoryId { get; set; }

        public string ContentManagerId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryShortDescription { get; set; }

        public string FeaturedImagePath { get; set; }

        public string FeaturedImageAltText { get; set; }

        public string ProductListPagePath { get; set; }

        public string HtmlContent { get; set; }

        public IList<GetBrandSubCategoriesResult> SubCategories { get; set; }

        public Pagination Pagination { get; set; }
    }
}
