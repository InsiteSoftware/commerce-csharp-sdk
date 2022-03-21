using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListQueryParameters : BaseQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = new List<string> {"top3products"};

        public int PageNumber { get; set; } = 1;

        public WishListSortOrder SortOrder { get; set; } = WishListSortOrder.ModifiedOnDescending;

        public string SearchText { get; set; } = null;

        /// <summary>
        /// Here are the values in the Exclude List.
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Exclude { get; set; }

        public int CurrentPage { get; set; }
    }

    public class CreateWishListQueryParameters : BaseQueryParameters
    {
        public WishList WishListObj { get; set; } = null;
    }
}
