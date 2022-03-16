using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class CategoryQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public Guid? startCategoryId { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int? maxDepth { get; set; } = null;
    }

    public class FeaturedCategoryQueryParameter : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public int? maxDepth { get; set; } = null;
    }
}
