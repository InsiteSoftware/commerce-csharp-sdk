using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListLineQueryParameters : BaseQueryParameters
    {
        public WishListLineSortOrder SortOrder = WishListLineSortOrder.CustomSort;

        public int DefaultPageSize;

        public string ChangedSharedListLinesQuantities;

        public string Query = null;
    }
}
