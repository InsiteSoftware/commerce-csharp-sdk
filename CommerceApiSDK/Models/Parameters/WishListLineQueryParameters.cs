using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListLineQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public int pageNumber = 1;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int pageSize = 16;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public WishListLineSortOrder sortOrder = WishListLineSortOrder.CustomSort;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string searchQuery = null;
    }
}
