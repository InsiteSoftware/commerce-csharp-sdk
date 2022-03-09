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
        public InvoiceService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<Invoice> GetInvoice(InvoiceDetailParameter parameters = null)
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

                Invoice result = await GetAsyncNoCache<Invoice>(url);
                if (result == null)
                {
                    throw new Exception("The invoice requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetInvoiceResult> GetInvoices(InvoiceQueryParameters parameters = null)
        {
            try
            {
                string url = parameters == null ? CommerceAPIConstants.InvoicesUrl : $"{CommerceAPIConstants.InvoicesUrl}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetInvoiceResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> SendEmail(InvoiceEmailParameter parameters = null)
        {
            try
            {
                string url = $"{CommerceAPIConstants.InvoicesUrl}/shareinvoice";
                StringContent stringContent = await Task.Run(() => SerializeModel(parameters));

                return await PostAsyncNoResult(url, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }
    }
}