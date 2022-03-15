using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<GetWarehouseCollectionResult> GetWarehouses(double latitude = 0, double longitude = 0, int pageNumber = 1, int pageSize = 16);
    }
}