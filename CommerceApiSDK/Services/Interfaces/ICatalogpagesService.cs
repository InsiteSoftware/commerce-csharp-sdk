using CommerceApiSDK.Models;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ICatalogpagesService
    {
        Task<ServiceResponse<CatalogPage>> GetProductCatalogInformation(string productPath);
    }
}
