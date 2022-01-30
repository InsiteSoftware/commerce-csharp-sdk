namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class InvoiceService : ServiceBase, IInvoiceService
    {
        private const string InvoicesUrl = "/api/v1/invoices";

        public InvoiceService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
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

                var url = $"{InvoicesUrl}/{parameters.InvoiceNumber}";

                if (parameters?.Expand != null)
                {
                    var queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var result = await this.GetAsyncNoCache<Invoice>(url);
                if (result == null)
                {
                    throw new Exception("The invoice requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetInvoiceResult> GetInvoices(InvoiceQueryParameters parameters = null)
        {
            try
            {
                var url = parameters == null ? InvoicesUrl : $"{InvoicesUrl}{parameters.ToQueryString()}";
                return await this.GetAsyncNoCache<GetInvoiceResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> SendEmail(InvoiceEmailParameter parameters = null)
        {
            try
            {
                var url = $"{InvoicesUrl}/shareinvoice";
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(parameters));

                return await this.PostAsyncNoResult(url, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }
    }
}