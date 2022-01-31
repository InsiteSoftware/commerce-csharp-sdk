namespace CommerceApiSDK.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Country : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the abbreviation.</summary>
        public string Abbreviation { get; set; }

        /// <summary>Gets or sets the states.</summary>
        public IList<State> States { get; set; }
    }

    public class CountryCollectionModel : BaseModel
    {
        public IList<Country> Countries { get; set; }
    }

    public class State : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the abbreviation.</summary>
        public string Abbreviation { get; set; }

        /// <summary>Gets or sets the states.</summary>
        public IList<State> States { get; set; }
    }

    public class StateCollectionModel : BaseModel
    {
        /// <summary>Gets or sets the states.</summary>
        public IList<State> States { get; set; }
    }

    public class Language : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the language code.</summary>
        public string LanguageCode { get; set; }

        /// <summary>Gets or sets the culture code.</summary>
        public string CultureCode { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the image file path.</summary>
        public string ImageFilePath { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is default.</summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is enabled on this site.</summary>
        public bool IsLive { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }

    public class LanguageCollectionModel : BaseModel
    {
        public IList<Language> Languages { get; set; }
    }

    public class Currency : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        public string CurrencyCode { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the currency symbol.</summary>
        public string CurrencySymbol { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is default.</summary>
        public bool IsDefault { get; set; }
    }

    public class CurrencyCollectionModel : BaseModel
    {
        public IList<Currency> Currencies { get; set; }
    }

    public class Website : BaseModel
    {
        /// <summary>Gets or sets the countries URI.</summary>
        public string CountriesUri { get; set; }

        /// <summary>Gets or sets the states URI.</summary>
        public string StatesUri { get; set; }

        /// <summary>Gets or sets the languages URI.</summary>
        public string LanguagesUri { get; set; }

        /// <summary>Gets or sets the currencies URI.</summary>
        public string CurrenciesUri { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is active.</summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is restricted.</summary>
        public bool IsRestricted { get; set; }

        /// <summary>Gets or sets the countries.</summary>
        public CountryCollectionModel Countries { get; set; }

        /// <summary>Gets or sets the states.</summary>
        public StateCollectionModel States { get; set; }

        /// <summary>Gets or sets the languages.</summary>
        public LanguageCollectionModel Languages { get; set; }

        /// <summary>Gets or sets the currencies.</summary>
        public CurrencyCollectionModel Currencies { get; set; }

        /// <summary>Gets or sets the mobile primary color</summary>
        public string MobilePrimaryColor { get; set; }

        /// <summary>Gets or sets the mobile privacy policy url</summary>
        public string MobilePrivacyPolicyUrl { get; set; }

        /// <summary>Gets or sets the mobile terms of use url</summary>
        public string MobileTermsOfUseUrl { get; set; }
    }

    public class WebsiteCrosssells : BaseModel
    {
        public IList<Product> Products { get; set; }
    }

    public class WebsiteCountries : BaseModel
    {
        public IList<Country> Countries { get; set; }
    }
}
