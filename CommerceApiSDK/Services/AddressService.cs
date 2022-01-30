namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class AddressService : ServiceBase, IAddressService
    {
        private const string BillToToUrl = "/api/v1/billtos";
        private string ShipToToUrl(string billToId)
        {
            return $"/api/v1/billtos/{billToId}/shiptos";
        }

        public AddressService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<GetBillTosResult> GetBillToAddressesAsync(string searchText, int pageNumber = 1, int pageSize = 16, bool excludeShowingAll = true)
        {
            try
            {
                var url = BillToToUrl;
                var parameters = new List<string>
                {
                    "parameter.page=" + pageNumber,
                    "parameter.pageSize=" + pageSize,
                };
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    parameters.Add("parameter.filter=" + WebUtility.UrlEncode(searchText));
                }

                var expandParameters = new List<string>();
                if (excludeShowingAll)
                {
                    expandParameters.Add("excludeshowall");
                }

                if (expandParameters.Count > 0)
                {
                    parameters.Add("parameter.expand=" + string.Join(",", expandParameters));
                }

                url += "?" + string.Join("&", parameters);
                return await this.GetAsyncNoCache<GetBillTosResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetShipTosResult> GetShipToAddressesAsync(string billToId, string searchText = null, int pageNumber = 1, int pageSize = 16, bool excludeShowingAll = true)
        {
            try
            {
                var url = this.ShipToToUrl(billToId);
                var parameters = new List<string>
                {
                    "apiParameter.page=" + pageNumber,
                    "apiParameter.pageSize=" + pageSize,
                    "apiParameter.billToId=" + billToId,
                };

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    parameters.Add("apiParameter.filter=" + WebUtility.UrlEncode(searchText));
                }

                var expandParameters = new List<string>();
                if (excludeShowingAll)
                {
                    expandParameters.Add("excludeshowall");
                }

                if (expandParameters.Count > 0)
                {
                    parameters.Add("apiParameter.expand=" + string.Join(",", expandParameters));
                }

                url += "?" + string.Join("&", parameters);
                return await this.GetAsyncNoCache<GetShipTosResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ShipTo> PostShipToAddressAsync(string billToId, ShipTo shipTo)
        {
            try
            {
                var url = this.ShipToToUrl(billToId);
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(shipTo));

                var result = await this.PostAsyncNoCache<ShipTo>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<ShipTo> GetShipToAddress(Guid billToId, Guid shipToId)
        {
            try
            {
                var url = $"{BillToToUrl}/{billToId}/shiptos/{shipToId}";
                return await this.GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
