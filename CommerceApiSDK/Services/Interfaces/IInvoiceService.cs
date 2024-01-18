using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<GetInvoiceResult>> GetInvoices(
            InvoiceQueryParameters parameters = null
        );

        Task<ServiceResponse<Invoice>> GetInvoice(InvoiceDetailParameter parameters = null);

        Task<ServiceResponse<bool>> SendEmail(InvoiceEmailParameter parameters = null);
    }
}
