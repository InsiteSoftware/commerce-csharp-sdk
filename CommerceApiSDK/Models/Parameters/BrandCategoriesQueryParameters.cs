namespace CommerceApiSDK.Models.Parameters
{
    using System;

    public class BrandCategoriesQueryParameter : BaseQueryParameters
    {
        public Guid? BrandId { get; set; }

        public Guid? CategoryId { get; set; }

        public string ProductListPagePath { get; set; }

        public int MaximumDepth { get; set; } = 2;
    }
}
