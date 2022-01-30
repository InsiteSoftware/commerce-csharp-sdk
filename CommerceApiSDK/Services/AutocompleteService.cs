namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class AutocompleteService : ServiceBase, IAutocompleteService
    {
        private const string AutocompleteUrl = "/api/v1/autocomplete";

        public AutocompleteService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService)
        {
        }

        public async Task<IList<AutocompleteBrand>> GetAutocompleteBrands(string searchQuery)
        {
            var parameters = new AutocompleteQueryParameters
            {
                Query = searchQuery,
                BrandEnabled = true,
                CategoryEnabled = false,
                ContentEnabled = false,
                ProductEnabled = false,
            };

            var results = await this.GetAutocompleteResults(parameters);
            return results?.Brands;
        }

        public async Task<IList<AutocompleteProduct>> GetAutocompleteProducts(string searchQuery)
        {
            try
            {
                var url = AutocompleteService.AutocompleteUrl;
                var parameters = new List<string>
                {
                    "query=" + searchQuery,
                    "categoryEnabled=false",
                    "contentEnabled=false",
                    "productEnabled=true",
                    "brandEnabled=false",
                };

                url += "?" + string.Join("&", parameters);

                var result = await this.GetAsyncWithCachedResponse<Autocomplete>(url);

                return result?.Products;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AutocompleteResult> GetAutocompleteResults(AutocompleteQueryParameters parameters)
        {
            try
            {
                return await this.GetAsyncWithCachedResponse<AutocompleteResult>(AutocompleteUrl + parameters.ToQueryString());
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
