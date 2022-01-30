namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;

    /// <summary>
    /// A service which fetches customer addresses
    /// </summary>
    public interface IAddressService
    {
        Task<GetBillTosResult> GetBillToAddressesAsync(
            string searchText,
            int pageNumber = 1,
            int pageSize = 16,
            bool excludeShowingAll = true);

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
    }
}
