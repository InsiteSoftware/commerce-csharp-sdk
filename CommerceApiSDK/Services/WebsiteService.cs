using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApiSDK.Extensions;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Parameters;
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

        public async Task<Website> GetWebsite(WebsiteQueryParameters parameters = null)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesUrl}";
                if (parameters != null)
                {
                    url += parameters.ToQueryString();
                }

                Website website = await GetAsyncWithCachedResponse<Website>(
                    url,
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

        public async Task<AddressFieldCollection> GetAddressFields()
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesAddressFieldsUrl}";

                return await GetAsyncWithCachedResponse<AddressFieldCollection>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<CountryCollection> GetCountries(CountriesQueryParameters parameters = null)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesCountriesUrl}";
                if (parameters != null)
                {
                    url += parameters.ToQueryString();
                }

                return await GetAsyncWithCachedResponse<CountryCollection>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Country> GetCountry(Guid countryId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesCountriesUrl}/{countryId}";

                return await GetAsyncWithCachedResponse<Country>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<WebsiteCrosssells> GetCrosssells()
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

        public async Task<CurrencyCollection> GetCurrencies()
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesCurrenciesUrl}";

                return await GetAsyncWithCachedResponse<CurrencyCollection>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Currency> GetCurrency(Guid currencyId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesCurrenciesUrl}/{currencyId}";

                return await GetAsyncWithCachedResponse<Currency>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<LanguageCollection> GetLanguages()
        {
            try
            {
                return await GetAsyncWithCachedResponse<LanguageCollection>(
                    CommerceAPIConstants.WebsitesLanguagesUrl
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Language> GetLanguage(Guid languageId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesLanguagesUrl}/{languageId}";

                return await GetAsyncWithCachedResponse<Language>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
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

        public async Task<StateCollection> GetStates()
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesStatesUrl}";

                return await GetAsyncWithCachedResponse<StateCollection>(
                    url
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<State> GetState(Guid stateId)
        {
            try
            {
                string url = $"{CommerceAPIConstants.WebsitesStatesUrl}/{stateId}";

                return await GetAsyncWithCachedResponse<State>(
                    url
                );
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
    }
}
