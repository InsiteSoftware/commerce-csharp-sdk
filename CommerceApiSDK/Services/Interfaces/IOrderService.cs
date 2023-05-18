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

        Task<ServiceResponse<GetOrderCollectionResult>> GetOrders(OrdersQueryParameters parameters = null);

        Task<ServiceResponse<GetOrderApprovalCollectionResult>> GetOrderApprovalList(OrderApprovalQueryParameters parameters = null);

        Task<ServiceResponse<Order>> GetOrder(string orderNumber);

        Task<ServiceResponse<Order>> PatchOrder(Order order);

        Task<ServiceResponse<Rma>> PostOrderReturns(string orderId, Rma rmaReturn);

        Task<ServiceResponse<ShareEntity>> ShareOrder(ShareOrder order);

        Task<ServiceResponse<List<OrderStatusMapping>>> GetOrderStatusMappings();

        List<string> SelectedFilterValueIds { get; set; }

        int SelectedFiltersCount { get; set; }
    }
}
