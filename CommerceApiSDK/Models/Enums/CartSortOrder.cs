using System;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models.Enums
{
    public enum CartSortOrder
    {
        [SortOrder("Order Date", "Order Date \u2193", "OrderDate DESC")]
        OrderDateDescending,

        [SortOrder("Order Date", "Order Date \u2191", "OrderDate ASC")]
        OrderDateAscending,

        [SortOrder("Order SubTotal", "Order SubTotal \u2193", "OrderSubTotal DESC")]
        OrderSubTotalDescending,

        [SortOrder("Order SubTotal", "Order SubTotal \u2191", "OrderSubTotal ASC")]
        OrderSubTotalAscending
    }
}
