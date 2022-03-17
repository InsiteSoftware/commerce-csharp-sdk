using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListLineQueryParameters : BaseQueryParameters
    {
        public int pageNumber = 1;

        public int pageSize = 16;

        public WishListLineSortOrder sortOrder = WishListLineSortOrder.CustomSort;

        public string searchQuery = null;
    }
}
