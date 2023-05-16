using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class AutocompleteService : ServiceBase, IAutocompleteService
    {
        public AutocompleteService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<IList<AutocompleteBrand>>> GetAutocompleteBrands(string searchQuery)
        {
            AutocompleteQueryParameters parameters = new AutocompleteQueryParameters()
            {
                Query = searchQuery,
                BrandEnabled = true,
                CategoryEnabled = false,
                ContentEnabled = false,
                ProductEnabled = false,
            };

            var results = await GetAutocompleteResults(parameters);
            return new ServiceResponse<IList<AutocompleteBrand>>()
            {
                Model = results.Model?.Brands,
                Error = results.Error,
                Exception = results.Exception,
                StatusCode = results.StatusCode,
                IsCached = results.IsCached
            };
        }

        public async Task<ServiceResponse<IList<AutocompleteProduct>>> GetAutocompleteProducts(string searchQuery)
        {
            try
            {
                string url = CommerceAPIConstants.AutocompleteUrl;
                List<string> parameters = new List<string>()
                {
                    "query=" + searchQuery,
                    "categoryEnabled=false",
                    "contentEnabled=false",
                    "productEnabled=true",
                    "brandEnabled=false",
                };

                url += "?" + string.Join("&", parameters);

                var result = await GetAsyncWithCachedResponse<Autocomplete>(url);

                return new ServiceResponse<IList<AutocompleteProduct>>() {
                    Model = result.Model?.Products,
                    Exception = result.Exception,
                    Error = result.Error,
                    StatusCode = result.StatusCode,
                    IsCached = result.IsCached,
                };
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<IList<AutocompleteProduct>>(exception: exception);
            }
        }

        public async Task<ServiceResponse<AutocompleteResult>> GetAutocompleteResults(
            AutocompleteQueryParameters parameters
        )
        {
            try
            {
                return await GetAsyncWithCachedResponse<AutocompleteResult>(
                    CommerceAPIConstants.AutocompleteUrl + parameters.ToQueryString()
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<AutocompleteResult>(exception: exception);
            }
        }
    }
}
