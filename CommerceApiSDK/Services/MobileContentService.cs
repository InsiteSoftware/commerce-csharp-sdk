﻿using System.Threading.Tasks;
using CommerceApiSDK.Models.ContentManagement;
using CommerceApiSDK.Models.ContentManagement.Pages;
using CommerceApiSDK.Services.Interfaces;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
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

            string url = string.Format(mobileContentUrlFormat, pageName);

            PageContentManagement result;
            if (useCache)
            {
                result = await GetAsyncWithCachedResponse<PageContentManagement>(url, DefaultRequestTimeout, new JsonConverter[] { new WidgetConverter(), new ActionConverter() });
            }
            else
            {
                result = await GetAsyncNoCache<PageContentManagement>(url, DefaultRequestTimeout, new JsonConverter[] { new WidgetConverter(), new ActionConverter() });
            }

            return result;
        }

        public async Task<string> GetPageContenManagmentString(string pageName, bool useCache = true)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                return null;
            }

            string url = string.Format(mobileContentUrlFormat, pageName);

            string result;

            if (useCache)
            {
                result = await GetAsyncStringResultWithCachedResponse(url, DefaultRequestTimeout);
            }
            else
            {
                result = await GetAsyncStringResultNoCache(url, DefaultRequestTimeout);
            }

            return result;
        }
    }
}