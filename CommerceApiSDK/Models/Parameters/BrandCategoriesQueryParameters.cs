using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class BrandCategoriesQueryParameter : BaseQueryParameters
    {
        public Guid? BrandId { get; set; }

        public Guid? CategoryId { get; set; }

        public string ProductListPagePath { get; set; }

        public int MaximumDepth { get; set; } = 2;
    }
}
