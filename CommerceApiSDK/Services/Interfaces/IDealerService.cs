using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IDealerService
    {
        /// <summary>
        /// Request dealer list from api. Consider using try catch to handle exception.
        /// </summary>
        /// <param name="parameters">Request parameters.</param>
        /// <param name="cancellationToken">Cancellation Token if needed.</param>
        /// <returns>GetDealerCollectionResult.</returns>
        Task<GetDealerCollectionResult> GetDealers(DealerLocationFinderQueryParameters parameters, CancellationToken? cancellationToken = null);
    }
}
