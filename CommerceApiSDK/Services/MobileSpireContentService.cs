using System.Threading.Tasks;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class MobileSpireContentService : ServiceBase, IMobileSpireContentService
    {
        public MobileSpireContentService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<string> GetPageContenManagmentString(
            string pageName,
            bool useCache = true
        )
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            string url = $"{CommerceAPIConstants.contentUrl}{pageName}";

            this.LoggerService.LogConsole(LogLevel.INFO, "Response content: {0}", url);

            return useCache
              ? await GetAsyncStringResultWithCachedResponse(url)
              : await GetAsyncStringResultNoCache(url);
        }
    }
}
