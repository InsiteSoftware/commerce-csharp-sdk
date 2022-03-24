using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DealerService : ServiceBase, IDealerService
    {
        public DealerService(ICommerceAPIServiceProvider commerceAPIServiceProvider)
         : base(commerceAPIServiceProvider)
        {
        }

        public async Task<GetDealerCollectionResult> GetDealers(DealerLocationFinderQueryParameters parameters, CancellationToken? cancellationToken = null)
        {
            string url = $"{CommerceAPIConstants.DealersUrl}{parameters?.ToQueryString() ?? string.Empty}";

            return await GetAsyncWithCachedResponse<GetDealerCollectionResult>(url, null, null, cancellationToken);
        }
    }
}
