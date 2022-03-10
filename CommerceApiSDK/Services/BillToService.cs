using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class BillToService : ServiceBase, IBillToService
    {
        private const string BillTosUrl = "/api/v1/billtos";

        private static string ShipTosUrl(Guid billToId) => $"/api/v1/billtos/{billToId}/shiptos";

        private static string BillToIdUrl(Guid billToId) => $"/api/v1/billtos/{billToId}";

        private static string ShipToIdUrl(Guid billToId, Guid shipToId) => $"/api/v1/billtos/{billToId}/shiptos/{shipToId}";

        public BillToService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<GetBillTosResult> GetBillTosAsync(BillTosQueryParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = BillTosUrl + queryString;
                return await GetAsyncNoCache<GetBillTosResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<BillTo> PostBillTosAsync(BillTo billTo)
        {
            try
            {
                var url = BillTosUrl;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PostAsyncNoCache<BillTo>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<BillTo> GetBillTo(Guid billToId)
        {
            try
            {
                string url = BillToIdUrl(billToId);
                return await GetAsyncNoCache<BillTo>(url);

            }
            catch (Exception e)
            {
                TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<BillTo> PatchBillTo(Guid billToId, BillTo billTo)
        {
            try
            {
                var url = BillToIdUrl(billToId);
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PatchAsyncNoCache<BillTo>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }
            
        public async Task<GetShipTosResult> GetShipTosAsync(Guid billToId, ShipTosQueryParameters parameters = null)
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = ShipTosUrl(billToId) + queryString;
                return await GetAsyncNoCache<GetShipTosResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ShipTo> PostShipToAsync(Guid billToId, ShipTo shipTo)
        {
            try
            {
                string url = ShipTosUrl(billToId);
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

        public async Task<ShipTo> GetShipTo(Guid billToId, Guid shipToId)
        {
            try
            {
                string url = ShipToIdUrl(billToId, shipToId);
                return await GetAsyncNoCache<ShipTo>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ShipTo> PatchShipTo(Guid billToId, Guid shipToId, ShipTo shipTo)
        {
            try
            {
                string url = ShipToIdUrl(billToId, shipToId);
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
