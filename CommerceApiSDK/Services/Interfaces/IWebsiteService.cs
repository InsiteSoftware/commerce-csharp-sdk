using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task<Website> GetWebsite(WebsiteQueryParameters parameters = null);

        Task<AddressFieldCollection> GetAddressFields();

        Task<CountryCollection> GetCountries(CountriesQueryParameters parameters = null);

        Task<Country> GetCountry(Guid countryId);

        Task<WebsiteCrosssells> GetCrosssells();

        Task<CurrencyCollection> GetCurrencies();

        Task<Currency> GetCurrency(Guid currencyId);
        
        Task<LanguageCollection> GetLanguages();

        Task<Language> GetLanguage(Guid languageId);

        Task<GetSiteMessageCollectionResult> GetSiteMessages(List<string> names = null);

        Task<StateCollection> GetStates();

        Task<State> GetState(Guid stateId);

        /// <summary>
        /// Get full URL for path with domain, access token and customer ids
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthorizedURL(string path);

        Task<bool> HasWebsiteCache();

        Task<bool> HasWebsiteCrosssellsCache();

        Task<string> GetSiteMessage(string messageName, string defaultMessage = null);
    }
}
