using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListQueryParameters : BaseQueryParameters
    {
        public string expand { get; set; } = "top3products";

        public int pageNumber { get; set; } = 1;

        public int pageSize { get; set; } = 16;

        public WishListSortOrder sortOrder { get; set; } = WishListSortOrder.ModifiedOnDescending;

        public string searchText { get; set; } = null;
    }

    public class CreateWishListQueryParameters : BaseQueryParameters
    {
        public string wishListName { get; set; }

        public string description { get; set; } = "";
    }
}
