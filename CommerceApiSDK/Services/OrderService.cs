using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Attributes;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        public OrderService(ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
        {
        }

        /// <summary>
        /// Gets all available sort order options.
        /// </summary>
        public List<OrderSortOrder> AvailableSortOrders => Enum
                    .GetValues(typeof(OrderSortOrder))
                    .Cast<OrderSortOrder>()
                    .Where(o => SortOrderAttribute.GetSortOrderOption(o) != SortOrderOptions.DoNotDisplay)
                    .ToList();

        /// <summary>
        /// Gets order list for api with selected order.
        /// </summary>
        /// <param name="sortOrder">target order.</param>
        /// <returns>list of orders for request api.</returns>
        public List<OrderSortOrder> GetOrderListForRequest(OrderSortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case OrderSortOrder.OrderDateAscending:
                    return new List<OrderSortOrder> { sortOrder, OrderSortOrder.OrderERPNumberAscending, OrderSortOrder.OrderNumberAscending };
                case OrderSortOrder.OrderDateDescending:
                    return new List<OrderSortOrder> { sortOrder, OrderSortOrder.OrderERPNumberDescending, OrderSortOrder.OrderNumberDescending };
                default:
                    return new List<OrderSortOrder> { sortOrder };
            }
        }

        public async Task<GetOrderCollectionResult> GetOrders(OrdersQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url = $"{CommerceAPIConstants.OrdersUrl}{parameters.ToQueryString()}";

                return await GetAsyncWithCachedResponse<GetOrderCollectionResult>(url);
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<Order> GetOrder(string orderNumber)
        {
            try
            {
                string url = CommerceAPIConstants.OrdersUrl + "/" + orderNumber + "?expand=orderlines,shipments";

                return await GetAsyncWithCachedResponse<Order>(url);
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<List<OrderStatusMapping>> GetOrderStatusMappings()
        {
            try
            {
                string url = CommerceAPIConstants.OrderStatusMappingsUrl;

                GetOrderStatusMappingsResult result = await GetAsyncWithCachedResponse<GetOrderStatusMappingsResult>(url);
                return result?.OrderStatusMappings;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        private List<string> selectedFilterValueIds;
        List<string> IOrderService.SelectedFilterValueIds
        {
            get => selectedFilterValueIds ?? new List<string>();
            set => selectedFilterValueIds = value;
        }

        private int selectedFiltersCount;
        int IOrderService.SelectedFiltersCount
        {
            get => selectedFiltersCount;
            set => selectedFiltersCount = value;
        }
    }
}