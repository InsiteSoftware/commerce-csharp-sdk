namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class DealerService : ServiceBase, IDealerService
    {
        private const string DealersUrl = "/api/v1/dealers";

        public DealerService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
         : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<GetDealerCollectionResult> GetDealers(DealerLocationFinderQueryParameters parameters, CancellationToken? cancellationToken = null)
        {
            var url = $"{DealersUrl}{parameters?.ToQueryString() ?? string.Empty}";

            return await this.GetAsyncWithCachedResponse<GetDealerCollectionResult>(url, null, null, cancellationToken);
        }
    }
}
