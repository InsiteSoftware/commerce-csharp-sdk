using System;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models.Enums
{
    public enum WishListSortOrder
    {
        [SortOrder("Date Updated", "Date Updated \u2713", "ModifiedOn DESC")]
        ModifiedOnDescending,

        [SortOrder("List Name", "List Name \u2193", "Name DESC")]
        NameDescending,

        [SortOrder("List Name", "List Name \u2191", "Name ASC")]
        NameAscending,
    }
}
