namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;

    public interface IInvoiceService
    {
        Task<GetInvoiceResult> GetInvoices(InvoiceQueryParameters parameters = null);

        Task<Invoice> GetInvoice(InvoiceDetailParameter parameters = null);

        Task<bool> SendEmail(InvoiceEmailParameter parameters = null);
    }
}