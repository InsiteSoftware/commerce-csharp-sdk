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
        private static string ShipTosUrl(Guid billToId) =>
            $"{CommerceAPIConstants.BillTosUrl}/{billToId}/shiptos";

        private static string BillToIdUrl(Guid billToId) =>
            $"{CommerceAPIConstants.BillTosUrl}/{billToId}";

        private static string ShipToIdUrl(Guid billToId, Guid shipToId) =>
            $"{CommerceAPIConstants.BillTosUrl}/{billToId}/shiptos/{shipToId}";

        public BillToService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<GetBillTosResult> GetBillTosAsync(
            BillTosQueryParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = CommerceAPIConstants.BillTosUrl + queryString;
                return await GetAsyncNoCache<GetBillTosResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<BillTo> PostBillTosAsync(BillTo billTo)
        {
            try
            {
                var url = CommerceAPIConstants.BillTosUrl;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PostAsyncNoCache<BillTo>(url, stringContent);

                return result;
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
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
                this.TrackingService.TrackException(e);
                return null;
            }
        }

        public async Task<BillTo> GetCurrentBillTo()
        {
            try
            {
                return await GetAsyncNoCache<BillTo>(CommerceAPIConstants.BillToCurrentUrl);
            }
            catch (Exception e)
            {
                this.TrackingService.TrackException(e);
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
                this.TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<BillTo> PatchCurrentBillTo(BillTo billTo)
        {
            try
            {
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(billTo));

                var result = await this.PatchAsyncNoCache<BillTo>(
                    CommerceAPIConstants.BillToCurrentUrl,
                    stringContent
                );

                return result;
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<GetShipTosResult> GetShipTosAsync(
            Guid billToId,
            ShipTosQueryParameters parameters = null
        )
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
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetShipTosResult> GetCurrentShipTos(
            ShipTosQueryParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.BillTosUrl}/current/shiptos" + queryString;
                return await GetAsyncNoCache<GetShipTosResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetShipTosResult> GetCurrentBillToShipTosAsync(
            ShipTosQueryParameters parameters = null
        )
        {
            try
            {
                string queryString = string.Empty;

                if (parameters != null)
                {
                    queryString = parameters.ToQueryString();
                }

                string url = $"{CommerceAPIConstants.BillToCurrentShipTosUrl}{queryString}";
                return await GetAsyncNoCache<GetShipTosResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
                this.TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<ShipTo> PostCurrentBillToShipToAsync(ShipTo shipTo)
        {
            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(shipTo));

                ShipTo result = await PostAsyncNoCache<ShipTo>(
                    CommerceAPIConstants.BillToCurrentShipTosUrl,
                    stringContent
                );

                return result;
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
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
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ShipTo> GetCurrentShipTo()
        {
            try
            {
                return await GetAsyncNoCache<ShipTo>(
                    CommerceAPIConstants.BillToCurrentShipToCurrentUrl
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
                return null;
            }
        }

        public async Task<ShipTo> PatchCurrentShipTo(ShipTo shipTo)
        {
            try
            {
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(shipTo));

                var result = await this.PostAsyncNoCache<ShipTo>(
                    CommerceAPIConstants.BillToCurrentShipToCurrentUrl,
                    stringContent
                );

                return result;
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
                return null;
            }
        }
    }
}
