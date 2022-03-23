using System.Threading.Tasks;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class MobileSpireContentService : ServiceBase, IMobileSpireContentService
    {
        public MobileSpireContentService(
            ICommerceAPIServiceProvider commerceAPIServiceProvider)
          : base(
                commerceAPIServiceProvider)
        {
        }

        public async Task<string> GetPageContenManagmentString(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            string url = $"{CommerceAPIConstants.contentUrl}{pageName}";

            _commerceAPIServiceProvider.GetLoggerService().LogConsole(LogLevel.INFO, "Response content: {0}", url);

            return useCache ? await GetAsyncStringResultWithCachedResponse(url) : await GetAsyncStringResultNoCache(url);
        }
    }
}
