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
    public class BillToService : ServiceBase, IBillToService
    {
        private const string BillToToUrl = "/api/v1/billtos";

        private string ShipToToUrl(string billToId)
        {
            return $"/api/v1/billtos/{billToId}/shiptos";
        }

        private string BillToIdUrl(string billToId)
        {
            return $"/api/v1/billtos/{billToId}";
        }

        public BillToService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
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

        public async Task<BillToResult> PostBillToAddressesAsync(BillToResult billTo)
        {
            try
            {
                var url = BillToToUrl;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PostAsyncNoCache<BillToResult>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<BillToResult> GetBillToAddress(string billToId)
        {
            try
            {
                string url = BillToIdUrl(billToId);
                return await GetAsyncNoCache<BillToResult>(url);

            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<BillToResult> PatchBillToAddress(string billToId, BillToResult billTo)
        {
            try
            {
                var url = BillToIdUrl(billToId);
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PatchAsyncNoCache<BillToResult>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                TrackingService.TrackException(ex);
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

        public async Task<ShipTo> PatchShipToAddress(string billToId, string shipToId, ShipTo shipTo)
        {
            try
            {
                string url = $"{BillToToUrl}/{billToId}/shiptos/{shipToId}";
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(shipTo));

                var result = await this.PostAsyncNoCache<ShipTo>(url, stringContent);

                return result;
            }

            catch(Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }
    }
}
