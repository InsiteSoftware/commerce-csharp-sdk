using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which fetches customer addresses
    /// </summary>
    public interface IBillToService
    {
        Task<GetBillTosResult> GetBillTosAsync(BillTosQueryParameters parameters = null);

        Task<BillTo> PostBillTosAsync(BillTo billTo);

        Task<BillTo> GetBillTo(Guid billToId);

        Task<BillTo> GetCurrentBillTo();

        Task<BillTo> PatchBillTo(Guid billToId, BillTo billTo);

        Task<BillTo> PatchCurrentBillTo(BillTo billTo);

        Task<GetShipTosResult> GetShipTosAsync(
            Guid billToId,
            ShipTosQueryParameters parameters = null
        );

        Task<GetShipTosResult> GetCurrentShipTos(ShipTosQueryParameters parameters = null);

        Task<GetShipTosResult> GetCurrentBillToShipTosAsync(
            ShipTosQueryParameters parameters = null
        );

        Task<ShipTo> PostShipToAsync(Guid billToId, ShipTo shipTo);

        Task<ShipTo> PostCurrentBillToShipToAsync(ShipTo shipTo);

        Task<ShipTo> GetShipTo(Guid billToId, Guid shipToId);

        Task<ShipTo> GetCurrentShipTo();

        Task<ShipTo> PatchShipTo(Guid billToId, Guid shipToId, ShipTo shipTo);

        Task<ShipTo> PatchCurrentShipTo(ShipTo shipTo);
    }
}
