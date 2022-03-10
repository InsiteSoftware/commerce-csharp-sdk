using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
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

}
