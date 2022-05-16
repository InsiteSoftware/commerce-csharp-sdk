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

        public WebsiteService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService,
            ISessionService sessionService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService)
        {
            this.sessionService = sessionService;
        }

        public async Task<Website> GetWebsite()
        {
            try
            {
                Website website = await GetAsyncWithCachedResponse<Website>(
                    CommerceAPIConstants.WebsitesUrl,
                    DefaultRequestTimeout
                );
                return website;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<bool> HasWebsiteCache()
        {
            string key =
                this.ClientService.Host
                + CommerceAPIConstants.WebsitesUrl
                + this.ClientService.SessionStateKey;
            return await this.CacheService.HasOnlineCache(key);
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<bool> HasWebsiteCrosssellsCache()
        {
            string key =
                this.ClientService.Host
                + CommerceAPIConstants.WebsitesCrossSellsUrl
                + this.ClientService.SessionStateKey;
            return await this.CacheService.HasOnlineCache(key);
        }

        public async Task<WebsiteCrosssells> GetWebsiteCrosssells()
        {
            try
            {
                return await GetAsyncWithCachedResponse<WebsiteCrosssells>(
                    CommerceAPIConstants.WebsitesCrossSellsUrl
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<string> GetAuthorizedURL(string path)
        {
            string result;
            try
            {
                // get domain + path
                bool isFullPath =
                    Uri.TryCreate(path, UriKind.Absolute, out Uri uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

                if (isFullPath)
                {
                    result = uri.AbsoluteUri;
                }
                else
                {
                    Uri domain = this.ClientService.Url;

                    if (string.IsNullOrEmpty(domain.AbsolutePath) || string.IsNullOrEmpty(path))
                    {
                        return null;
                    }

                    result = new Uri(domain, path).ToString();
                }

                // sign
                string token = await this.ClientService.GetAccessToken();
                string billTo = this.sessionService.CurrentSession?.BillTo?.Id;
                string shipTo = this.sessionService.CurrentSession?.ShipTo?.Id;
                string languageCode = this.sessionService.CurrentSession?.Language?.LanguageCode;
                string currencyCode = this.sessionService.CurrentSession?.Currency?.CurrencyCode;
                string linkChar = result.Contains("?") ? "&" : "?";

                result =
                    string.IsNullOrEmpty(token)
                    || string.IsNullOrEmpty(billTo)
                    || string.IsNullOrEmpty(shipTo)
                        ? $"{result}{linkChar}SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}"
                        : $"{result}{linkChar}access_token={token}&CurrentBillToId={billTo}&CurrentShipToId={shipTo}&SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}";
            }
            catch (Exception e)
            {
                this.LoggerService.LogConsole(
                    LogLevel.INFO,
                    $"Can not create uri with path {path} exception: {e.Message}"
                );
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
                GetSiteMessageCollectionResult siteMessagesResult =
                    await GetAsyncWithCachedResponse<GetSiteMessageCollectionResult>(url);
                return siteMessagesResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public async Task<string> GetSiteMessage(string messageName, string defaultMessage = null)
        {
            GetSiteMessageCollectionResult messageResult = await GetSiteMessages(
                new List<string> { messageName }
            );

            SiteMessage siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(
                x =>
                    x.Message != null
                    && (
                        !string.IsNullOrEmpty(x.LanguageCode)
                        && x.LanguageCode.Equals(
                            this.sessionService.CurrentSession?.Language?.LanguageCode,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
            );
            if (siteMessageItem != null)
            {
                return string.IsNullOrEmpty(siteMessageItem.Message)
                  ? defaultMessage
                  : siteMessageItem.Message.StripHtml();
            }
            else
            {
                siteMessageItem = messageResult?.SiteMessages.FirstOrDefault(
                    x => string.IsNullOrEmpty(x.LanguageCode) && x.Message != null
                );
                if (siteMessageItem != null)
                {
                    return string.IsNullOrEmpty(siteMessageItem.Message)
                      ? defaultMessage
                      : siteMessageItem.Message.StripHtml();
                }
            }

            return defaultMessage;
        }

        public async Task<IList<Country>> GetCountries()
        {
            try
            {
                WebsiteCountries result = await GetAsyncWithCachedResponse<WebsiteCountries>(
                    CommerceAPIConstants.WebsitesCountries
                );
                return result?.Countries;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public Task<LanguageCollectionModel> GetLanguages()
        {
            try
            {
                return GetAsyncWithCachedResponse<LanguageCollectionModel>(
                    CommerceAPIConstants.WebsitesLanguagesUrl
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
