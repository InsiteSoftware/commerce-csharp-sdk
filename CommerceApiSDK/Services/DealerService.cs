using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DealerService : ServiceBase, IDealerService
    {
        public DealerService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService)
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService)
        {
        }

        public async Task<GetDealerCollectionResult> GetDealers(DealerLocationFinderQueryParameters parameters, CancellationToken? cancellationToken = null)
        {
            string url = $"{CommerceAPIConstants.DealersUrl}{parameters?.ToQueryString() ?? string.Empty}";

            return await GetAsyncWithCachedResponse<GetDealerCollectionResult>(url, null, null, cancellationToken);
        }
    }
}
