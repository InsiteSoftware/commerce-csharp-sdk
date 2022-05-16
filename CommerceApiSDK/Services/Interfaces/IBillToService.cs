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

        Task<BillTo> PatchBillTo(Guid billToId, BillTo billTo);

        Task<GetShipTosResult> GetShipTosAsync(
            Guid billToId,
            ShipTosQueryParameters parameters = null
        );

        Task<ShipTo> PostShipToAsync(Guid billToId, ShipTo shipTo);

        Task<ShipTo> GetShipTo(Guid billToId, Guid shipToId);

        Task<ShipTo> PatchShipTo(Guid billToId, Guid shipToId, ShipTo shipTo);
    }
}
