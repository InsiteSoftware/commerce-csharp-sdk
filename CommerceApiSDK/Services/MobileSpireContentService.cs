using System.Threading.Tasks;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class MobileSpireContentService : ServiceBase, IMobileSpireContentService
    {
        private const string contentUrl = "/api/v2/content/pageByType?type=Mobile/";

        public MobileSpireContentService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
          : base(
                clientService,
                networkService,
                trackingService,
                cacheService,
                loggerService)
        {
        }

        public async Task<string> GetPageContenManagmentString(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            string url = $"{contentUrl}{pageName}";

            loggerService.LogConsole(LogLevel.INFO, "Response content: {0}");

            return useCache ? await GetAsyncStringResultWithCachedResponse(url) : await GetAsyncStringResultNoCache(url);
        }
    }
}
