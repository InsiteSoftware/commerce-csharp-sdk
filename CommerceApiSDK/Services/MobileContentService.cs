namespace CommerceApiSDK.Services
{
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.ContentManagement;
    using CommerceApiSDK.Models.ContentManagement.Pages;
    using CommerceApiSDK.Services.Interfaces;
    using Newtonsoft.Json;

    public class MobileContentService : ServiceBase, IMobileContentService
    {
        private const string mobileContentUrlFormat = "/api/v1/mobilecontent/{0}";

        public MobileContentService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
          : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<PageContentManagement> GetPageContenManagment(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            var url = string.Format(mobileContentUrlFormat, pageName);

            PageContentManagement result;
            if (useCache)
            {
                result = await this.GetAsyncWithCachedResponse<PageContentManagement>(url, ServiceBase.DefaultRequestTimeout, new JsonConverter[] { new WidgetConverter(), new ActionConverter() });
            }
            else
            {
                result = await this.GetAsyncNoCache<PageContentManagement>(url, ServiceBase.DefaultRequestTimeout, new JsonConverter[] { new WidgetConverter(), new ActionConverter() });
            }

            return result;
        }

        public async Task<string> GetPageContenManagmentString(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            var url = string.Format(mobileContentUrlFormat, pageName);

            string result;

            if (useCache)
            {
                result = await this.GetAsyncStringResultWithCachedResponse(url, ServiceBase.DefaultRequestTimeout);
            }
            else
            {
                result = await this.GetAsyncStringResultNoCache(url, ServiceBase.DefaultRequestTimeout);
            }

            return result;
        }
    }
}
