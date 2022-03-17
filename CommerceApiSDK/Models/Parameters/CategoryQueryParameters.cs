using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class CategoryQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid? startCategoryId { get; set; } = null;

        public int? maxDepth { get; set; } = null;
    }

    public class FeaturedCategoryQueryParameter : BaseQueryParameters
    {
        public int? maxDepth { get; set; } = null;
    }
}
