using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class AddressService : ServiceBase, IAddressService
    {
        private const string BillToToUrl = "/api/v1/billtos";
        private const string BillToToUrl1 = "/api/v1/billtos";

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
                string url = BillToToUrl;
                List<string> parameters = new List<string>()
                {
                    "parameter.page=" + pageNumber,
                    "parameter.pageSize=" + pageSize,
                };
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    parameters.Add("parameter.filter=" + WebUtility.UrlEncode(searchText));
                }

                List<string> expandParameters = new List<string>();
                if (excludeShowingAll)
                {
                    expandParameters.Add("excludeshowall");
                }

                if (expandParameters.Count > 0)
                {
                    parameters.Add("parameter.expand=" + string.Join(",", expandParameters));
                }

                url += "?" + string.Join("&", parameters);
                return await GetAsyncNoCache<GetBillTosResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetShipTosResult> GetShipToAddressesAsync(string billToId, string searchText = null, int pageNumber = 1, int pageSize = 16, bool excludeShowingAll = true)
        {
            try
            {
                string url = ShipToToUrl(billToId);
                List<string> parameters = new List<string>()
                {
                    "apiParameter.page=" + pageNumber,
                    "apiParameter.pageSize=" + pageSize,
                    "apiParameter.billToId=" + billToId,
                };

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    parameters.Add("apiParameter.filter=" + WebUtility.UrlEncode(searchText));
                }

                List<string> expandParameters = new List<string>();
                if (excludeShowingAll)
                {
                    expandParameters.Add("excludeshowall");
                }

                if (expandParameters.Count > 0)
                {
                    parameters.Add("apiParameter.expand=" + string.Join(",", expandParameters));
                }

                url += "?" + string.Join("&", parameters);
                return await GetAsyncNoCache<GetShipTosResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ShipTo> PostShipToAddressAsync(string billToId, ShipTo shipTo)
        {
            try
            {
                string url = ShipToToUrl(billToId);
                StringContent stringContent = await Task.Run(() => SerializeModel(shipTo));

                ShipTo result = await PostAsyncNoCache<ShipTo>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<ShipTo> GetShipToAddress(Guid billToId, Guid shipToId)
        {
            try
            {
                string url = $"{BillToToUrl}/{billToId}/shiptos/{shipToId}";
                return await GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
