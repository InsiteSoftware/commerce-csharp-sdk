using System;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models.Enums
{
    public enum OrderSortOrder
    {
        [SortOrder("Order Date", "Order Date \u2193", "OrderDate DESC")]
        OrderDateDescending,
        [SortOrder("Order Date", "Order Date \u2191", "OrderDate ASC")]
        OrderDateAscending,
        [SortOrder("Order Number", "Order Number \u2193", "WebOrderNumber DESC")]
        OrderNumberDescending,
        [SortOrder("Order Number", "Order Number \u2191", "WebOrderNumber ASC")]
        OrderNumberAscending,
        [SortOrder("Shipping Address", "Shipping Address \u2193", "CustomerSequence DESC")]
        ShippingAddressDescending,
        [SortOrder("Shipping Address", "Shipping Address \u2191", "CustomerSequence ASC")]
        ShippingAddressAscending,
        [SortOrder("Status", "Status \u2193", "Status DESC")]
        StatusDescending,
        [SortOrder("Status", "Status \u2191", "Status ASC")]
        StatusAscending,
        [SortOrder("PO Number", "PO Number \u2193", "CustomerPO DESC")]
        PoNumberDescending,
        [SortOrder("PO Number", "PO Number \u2191", "CustomerPO ASC")]
        PoNumberAscending,
        [SortOrder("Total", "Total \u2193", "OrderTotal DESC")]
        OrderTotalDescending,
        [SortOrder("Total", "Total \u2191", "OrderTotal ASC")]
        OrderTotalAscending,
        [SortOrder(
            "Order ERP Number",
            "Order ERP Number \u2193",
            "ErpOrderNumber DESC",
            SortOrderOptions.DoNotDisplay
        )]
        OrderERPNumberDescending,
        [SortOrder(
            "Order ERP Number",
            "Order ERP Number \u2191",
            "ErpOrderNumber ASC",
            SortOrderOptions.DoNotDisplay
        )]
        OrderERPNumberAscending,
    }
}
