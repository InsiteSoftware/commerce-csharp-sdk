using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class CategoryQueryParameters : BaseQueryParameters
    {
        public Guid? StartCategoryId { get; set; } = null;

        public int? MaxDepth { get; set; } = null;

        public bool IncludeStartCategory { get; set; }
    }
}
