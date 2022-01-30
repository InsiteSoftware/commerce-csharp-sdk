namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.Results;

    public interface IWarehouseService
    {
        Task<GetWarehouseCollectionResult> GetWarehouses(double latitude = 0, double longitude = 0, int pageNumber = 1, int pageSize = 16);
    }
}