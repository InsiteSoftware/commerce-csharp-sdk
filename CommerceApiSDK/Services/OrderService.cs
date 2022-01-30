namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Attributes;
    using CommerceApiSDK.Services.Interfaces;

    public class OrderService : ServiceBase, IOrderService
    {
        private const string OrdersUrl = "/api/v1/orders";
        private const string OrderStatusMappingsUrl = "/api/v1/orderstatusmappings";

        public OrderService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        /// <summary>
        /// Gets all available sort order options.
        /// </summary>
        public List<OrderSortOrder> AvailableSortOrders
        {
            get
            {
                return Enum
                    .GetValues(typeof(OrderSortOrder))
                    .Cast<OrderSortOrder>()
                    .Where(o => SortOrderAttribute.GetSortOrderOption(o) != SortOrderOptions.DoNotDisplay)
                    .ToList();
            }
        }

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

                var url = $"{OrdersUrl}{parameters.ToQueryString()}";

                return await this.GetAsyncWithCachedResponse<GetOrderCollectionResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Order> GetOrder(string orderNumber)
        {
            try
            {
                var url = OrdersUrl + "/" + orderNumber + "?expand=orderlines,shipments";

                return await this.GetAsyncWithCachedResponse<Order>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<List<OrderStatusMapping>> GetOrderStatusMappings()
        {
            try
            {
                var url = OrderStatusMappingsUrl;

                var result = await this.GetAsyncWithCachedResponse<GetOrderStatusMappingsResult>(url);
                return result?.OrderStatusMappings;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        private List<string> selectedFilterValueIds;
        List<string> IOrderService.SelectedFilterValueIds
        {
            get
            {
                return this.selectedFilterValueIds ?? new List<string>();
            }
            set
            {
                this.selectedFilterValueIds = value;
            }
        }

        private int selectedFiltersCount;
        int IOrderService.SelectedFiltersCount
        {
            get
            {
                return this.selectedFiltersCount;
            }
            set
            {
                this.selectedFiltersCount = value;
            }
        }
    }
}