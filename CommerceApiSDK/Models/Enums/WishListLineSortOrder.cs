using System;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models.Enums
{
    public enum WishListLineSortOrder
    {
        [SortOrder("Custom Sort", "Custom Sort \u2713", "sortorder")]
        CustomSort,
        [SortOrder("Date Added", "Date Added \u2713", "createdon+desc")]
        DateAdded,
        [SortOrder("Product Name", "Product Name \u2193", "product.shortdescription+desc")]
        ProductNameDescending,
        [SortOrder("Product Name", "Product Name \u2191", "product.shortdescription")]
        ProductNameAscending,
    }
}
