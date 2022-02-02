using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderSortOrder> AvailableSortOrders { get; }

        List<OrderSortOrder> GetOrderListForRequest(OrderSortOrder sortOrder);

        Task<GetOrderCollectionResult> GetOrders(OrdersQueryParameters parameters = null);

        Task<Order> GetOrder(string orderNumber);

        Task<List<OrderStatusMapping>> GetOrderStatusMappings();

        List<string> SelectedFilterValueIds { get; set; }

        int SelectedFiltersCount { get; set; }
    }

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

        [SortOrder("Order ERP Number", "Order ERP Number \u2193", "ErpOrderNumber DESC", SortOrderOptions.DoNotDisplay)]
        OrderERPNumberDescending,

        [SortOrder("Order ERP Number", "Order ERP Number \u2191", "ErpOrderNumber ASC", SortOrderOptions.DoNotDisplay)]
        OrderERPNumberAscending,
    }
}
