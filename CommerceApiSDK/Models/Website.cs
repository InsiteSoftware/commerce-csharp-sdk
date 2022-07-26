namespace CommerceApiSDK.Models
{
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
        public CountryCollection Countries { get; set; }

        /// <summary>Gets or sets the states.</summary>
        public StateCollection States { get; set; }

        /// <summary>Gets or sets the languages.</summary>
        public LanguageCollection Languages { get; set; }

        /// <summary>Gets or sets the currencies.</summary>
        public CurrencyCollection Currencies { get; set; }

        /// <summary>Gets or sets the mobile primary color</summary>
        public string MobilePrimaryColor { get; set; }

        /// <summary>Gets or sets the mobile privacy policy url</summary>
        public string MobilePrivacyPolicyUrl { get; set; }

        /// <summary>Gets or sets the mobile terms of use url</summary>
        public string MobileTermsOfUseUrl { get; set; }
    }
}
