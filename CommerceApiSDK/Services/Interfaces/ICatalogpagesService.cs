using CommerceApiSDK.Models;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ICatalogpagesService
    {
        Task<CatalogPage> GetProductCatalogInformation(string productPath);
    }
}
