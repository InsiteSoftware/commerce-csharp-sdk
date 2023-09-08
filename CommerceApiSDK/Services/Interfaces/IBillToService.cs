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
        Task<ServiceResponse<GetBillTosResult>> GetBillTosAsync(BillTosQueryParameters parameters = null);

        Task<ServiceResponse<BillTo>> PostBillTosAsync(BillTo billTo);

        Task<ServiceResponse<BillTo>> GetBillTo(Guid billToId);

        Task<ServiceResponse<BillTo>> GetCurrentBillTo();

        Task<ServiceResponse<BillTo>> PatchBillTo(Guid billToId, BillTo billTo);

        Task<ServiceResponse<BillTo>> PatchCurrentBillTo(BillTo billTo);

        Task<ServiceResponse<GetShipTosResult>> GetShipTosAsync(
            Guid billToId,
            ShipTosQueryParameters parameters = null
        );

        Task<ServiceResponse<GetShipTosResult>> GetCurrentShipTos(ShipTosQueryParameters parameters = null);

        Task<ServiceResponse<GetShipTosResult>> GetCurrentBillToShipTosAsync(
            ShipTosQueryParameters parameters = null
        );

        Task<ServiceResponse<ShipTo>> PostShipToAsync(Guid billToId, ShipTo shipTo);

        Task<ServiceResponse<ShipTo>> PostCurrentBillToShipToAsync(ShipTo shipTo);

        Task<ServiceResponse<ShipTo>> GetShipTo(Guid billToId, Guid shipToId);

        Task<ServiceResponse<ShipTo>> GetCurrentShipTo();

        Task<ServiceResponse<ShipTo>> PatchShipTo(Guid billToId, Guid shipToId, ShipTo shipTo);

        Task<ServiceResponse<ShipTo>> PatchCurrentShipTo(ShipTo shipTo);
    }
}
