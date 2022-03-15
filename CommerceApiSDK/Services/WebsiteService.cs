using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApiSDK.Extensions;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    /// <summary>
    /// Service to fetch website properties and crosssells
    /// </summary>
    public class WebsiteService : ServiceBase, IWebsiteService
    {
        private readonly ISessionService sessionService;
        private readonly ILoggerService loggerService;

        public WebsiteService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ISessionService sessionService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
            this.sessionService = sessionService;
            this.loggerService = loggerService;
        }

        public async Task<Website> GetWebsite()
        {
            try
            {
                Website website = await GetAsyncWithCachedResponse<Website>(CommerceAPIConstants.WebsitesUrl, DefaultRequestTimeout);
                return website;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> HasWebsiteCache()
        {
            return await HasCache(CommerceAPIConstants.WebsitesUrl);
        }

        public async Task<bool> HasWebsiteCrosssellsCache()
        {
            return await HasCache(CommerceAPIConstants.WebsitesCrosssellsUrl);
        }

        public async Task<WebsiteCrosssells> GetWebsiteCrosssells()
        {
            try
            {
                return await GetAsyncWithCachedResponse<WebsiteCrosssells>(CommerceAPIConstants.WebsitesCrosssellsUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<string> GetAuthorizedURL(string path)
        {
            string result;
            try
            {
                // get domain + path
                bool isFullPath = Uri.TryCreate(path, UriKind.Absolute, out Uri uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

                if (isFullPath)
                {
                    result = uri.AbsoluteUri;
                }
                else
                {
                    Uri domain = Client.Url;

                    if (string.IsNullOrEmpty(domain.AbsolutePath) || string.IsNullOrEmpty(path))
                    {
                        return null;
                    }

                    result = new Uri(domain, path).ToString();
                }

                // sign
                string token = await Client.GetAccessToken();
                string billTo = sessionService.CurrentSession?.BillTo?.Id;
                string shipTo = sessionService.CurrentSession?.ShipTo?.Id;
                string languageCode = sessionService.CurrentSession?.Language?.LanguageCode;
                string currencyCode = sessionService.CurrentSession?.Currency?.CurrencyCode;
                string linkChar = result.Contains("?") ? "&" : "?";

                result = string.IsNullOrEmpty(token) || string.IsNullOrEmpty(billTo) || string.IsNullOrEmpty(shipTo)
                    ? $"{result}{linkChar}SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}"
                    : $"{result}{linkChar}access_token={token}&CurrentBillToId={billTo}&CurrentShipToId={shipTo}&SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}";
            }
            catch (Exception e)
            {
                loggerService.LogConsole(LogLevel.INFO, $"Can not create uri with path {path} exception: {e.Message}");
                return null;
            }

            return result;
        }

        public async Task<GetSiteMessageCollectionResult> GetSiteMessages(List<string> names = null)
        {
            string url = CommerceAPIConstants.WebsitesSiteMessagesUrl;

            if (names != null)
            {
                url += "?parameter.name=" + string.Join(",", names);
            }

            try
            {
                GetSiteMessageCollectionResult siteMessagesResult = await GetAsyncWithCachedResponse<GetSiteMessageCollectionResult>(url);
                return siteMessagesResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<string> GetSiteMessage(string messageName, string defaultMessage = null)
        {
            GetSiteMessageCollectionResult messageResult = await GetSiteMessages(new List<string>
            {
                messageName
            });

            SiteMessage siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(x => x.Message != null && (!string.IsNullOrEmpty(x.LanguageCode) && x.LanguageCode.Equals(sessionService.CurrentSession?.Language?.LanguageCode, StringComparison.OrdinalIgnoreCase)));
            if (siteMessageItem != null)
            {
                return string.IsNullOrEmpty(siteMessageItem.Message) ? defaultMessage : siteMessageItem.Message.StripHtml();
            }
            else
            {
                siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(x => string.IsNullOrEmpty(x.LanguageCode) && x.Message != null);
                if (siteMessageItem != null)
                {
                    return string.IsNullOrEmpty(siteMessageItem.Message) ? defaultMessage : siteMessageItem.Message.StripHtml();
                }
            }

            return defaultMessage;
        }

        public async Task<IList<Country>> GetCountries()
        {
            try
            {
                WebsiteCountries result = await GetAsyncWithCachedResponse<WebsiteCountries>(CommerceAPIConstants.WebsitesCountries);
                return result?.Countries;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public Task<LanguageCollectionModel> GetLanguages()
        {
            try
            {
                return GetAsyncWithCachedResponse<LanguageCollectionModel>(CommerceAPIConstants.WebsitesLanguagesUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}