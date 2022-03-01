using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which fetches customer addresses
    /// </summary>
    public interface IBillToService
    {
        Task<GetBillTosResult> GetBillToAddressesAsync(
            string searchText,
            int pageNumber = 1,
            int pageSize = 16,
            bool excludeShowingAll = true);

        Task<BillToResult> PostBillToAddressesAsync(BillToResult billTo);

        Task<BillToResult> GetBillToAddress(string billToId);

        Task<BillToResult> PatchBillToAddress(string billToId, BillToResult billTo);

        Task<GetShipTosResult> GetShipToAddressesAsync(
            string billToId,
            string searchText = null,
            int pageNumber = 1,
            int pageSize = 16,
            bool excludeShowingAll = true);

        Task<ShipTo> PostShipToAddressAsync(
            string billToId,
            ShipTo shipTo);

        Task<ShipTo> GetShipToAddress(Guid billToId, Guid shipToId);

        Task<ShipTo> PatchShipToAddress(string billToId, string shipToId, ShipTo shipTo);
    }
}
