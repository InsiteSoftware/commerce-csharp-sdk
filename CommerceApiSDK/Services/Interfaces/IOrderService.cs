using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderSortOrder> AvailableSortOrders { get; }

        List<OrderSortOrder> GetOrderListForRequest(OrderSortOrder sortOrder);

        Task<GetOrderCollectionResult> GetOrders(OrdersQueryParameters parameters = null);

        Task<Order> GetOrder(string orderNumber);

        Task<Order> PatchOrder(Order order);

        Task<Rma> PostOrderReturns(string orderId, Rma rmaReturn);

        Task<ShareEntity> ShareOrder(ShareOrder order);

        Task<List<OrderStatusMapping>> GetOrderStatusMappings();

        List<string> SelectedFilterValueIds { get; set; }

        int SelectedFiltersCount { get; set; }
    }
}
