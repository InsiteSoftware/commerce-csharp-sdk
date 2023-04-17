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
        Task<ServiceResponse<Website>> GetWebsite(WebsiteQueryParameters parameters = null);

        Task<ServiceResponse<AddressFieldCollection>> GetAddressFields();

        Task<ServiceResponse<CountryCollection>> GetCountries(CountriesQueryParameters parameters = null);

        Task<ServiceResponse<Country>> GetCountry(Guid countryId);

        Task<ServiceResponse<WebsiteCrosssells>> GetCrosssells();

        Task<ServiceResponse<CurrencyCollection>> GetCurrencies();

        Task<ServiceResponse<Currency>> GetCurrency(Guid currencyId);
        
        Task<ServiceResponse<LanguageCollection>> GetLanguages();

        Task<ServiceResponse<Language>> GetLanguage(Guid languageId);

        Task<ServiceResponse<GetSiteMessageCollectionResult>> GetSiteMessages(List<string> names = null);

        Task<ServiceResponse<StateCollection>> GetStates();

        Task<ServiceResponse<State>> GetState(Guid stateId);

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
