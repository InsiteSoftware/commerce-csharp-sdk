using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApiSDK.Extensions;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Utils.Logger;

namespace CommerceApiSDK.Services
{
    /// <summary>
    /// Service to fetch website properties and crosssells
    /// </summary>
    public class WebsiteService : ServiceBase, IWebsiteService
    {
        private const string WebsitesUrl = "/api/v1/websites/current";
        private const string WebsitesCrosssellsUrl = "/api/v1/websites/current/crosssells";
        private const string WebsitesSiteMessagesUrl = "/api/v1/websites/current/sitemessages";
        private const string WebsitesCountries = "/api/v1/websites/current/countries?expand=states";
        private const string WebsitesLanguagesUrl = "/api/v1/websites/current/languages";

        private readonly ISessionService sessionService;

        public WebsiteService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ISessionService sessionService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
            this.sessionService = sessionService;
        }

        public async Task<Website> GetWebsite()
        {
            try
            {
                Website website = await GetAsyncWithCachedResponse<Website>(WebsitesUrl, DefaultRequestTimeout);
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
            return await HasCache(WebsitesUrl);
        }

        public async Task<bool> HasWebsiteCrosssellsCache()
        {
            return await HasCache(WebsitesCrosssellsUrl);
        }

        public async Task<WebsiteCrosssells> GetWebsiteCrosssells()
        {
            try
            {
                return await GetAsyncWithCachedResponse<WebsiteCrosssells>(WebsitesCrosssellsUrl);
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
                Logger.LogTrace($"Can not create uri with path {path} exception: {e.Message}");
                return null;
            }

            return result;
        }

        public async Task<GetSiteMessageCollectionResult> GetSiteMessages(List<string> names = null)
        {
            string url = WebsitesSiteMessagesUrl;

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

            // ability to set messages to blank, so validate is null only here
            SiteMessage siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(x => x.Message != null && (!string.IsNullOrEmpty(x.LanguageCode) && x.LanguageCode.Equals(sessionService.CurrentSession?.Language?.LanguageCode, StringComparison.OrdinalIgnoreCase)));
            if (siteMessageItem != null)
            {
                return siteMessageItem.Message.StripHtml();
            }
            else
            {
                siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(x => string.IsNullOrEmpty(x.LanguageCode) && x.Message != null);
                if (siteMessageItem != null)
                {
                    return siteMessageItem.Message.StripHtml();
                }
            }

            return defaultMessage;
        }

        public async Task<IList<Country>> GetCountries()
        {
            try
            {
                WebsiteCountries result = await GetAsyncWithCachedResponse<WebsiteCountries>(WebsitesCountries);
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
                return GetAsyncWithCachedResponse<LanguageCollectionModel>(WebsitesLanguagesUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}