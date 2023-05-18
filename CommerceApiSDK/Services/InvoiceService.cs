using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class InvoiceService : ServiceBase, IInvoiceService
    {
        public InvoiceService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<Invoice>> GetInvoice(InvoiceDetailParameter parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url = $"{CommerceAPIConstants.InvoicesUrl}/{parameters.InvoiceNumber}";

                if (parameters?.Expand != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var result = await GetAsyncNoCache<Invoice>(url);
                if (result == null)
                {
                    throw new Exception("The invoice requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<Invoice>(exception: exception);
            }
        }

        public async Task<ServiceResponse<GetInvoiceResult>> GetInvoices(InvoiceQueryParameters parameters = null)
        {
            try
            {
                string url =
                    parameters == null
                        ? CommerceAPIConstants.InvoicesUrl
                        : $"{CommerceAPIConstants.InvoicesUrl}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetInvoiceResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetInvoiceResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<bool>> SendEmail(InvoiceEmailParameter parameters = null)
        {
            try
            {
                string url = $"{CommerceAPIConstants.InvoicesUrl}/shareinvoice";
                StringContent stringContent = await Task.Run(() => SerializeModel(parameters));

                return await PostAsyncNoResult(url, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return new ServiceResponse<bool>
                {
                    Exception = exception
                };
            }
        }
    }
}
