using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public string expand { get; set; } = "top3products";

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int pageNumber { get; set; } = 1;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public int pageSize { get; set; } = 16;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public WishListSortOrder sortOrder { get; set; } = WishListSortOrder.ModifiedOnDescending;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string searchText { get; set; } = null;
    }

    public class CreateWishListQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public string wishListName { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string description { get; set; } = "";
    }
}
