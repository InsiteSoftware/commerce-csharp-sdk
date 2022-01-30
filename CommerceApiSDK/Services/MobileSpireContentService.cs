namespace CommerceApiSDK.Services
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Utils.Logger;

    public class MobileSpireContentService : ServiceBase, IMobileSpireContentService
    {
        private const string contentUrl = "/api/v2/content/pageByType?type=Mobile/";

        public MobileSpireContentService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
          : base(
                clientService,
                networkService,
                trackingService,
                cacheService)
        {
        }

        public async Task<string> GetPageContenManagmentString(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            var url = $"{contentUrl}{pageName}";
            Logger.LogTrace("Response content: {0}", url);

            return useCache ? await this.GetAsyncStringResultWithCachedResponse(url) : await this.GetAsyncStringResultNoCache(url);
        }
    }
}
