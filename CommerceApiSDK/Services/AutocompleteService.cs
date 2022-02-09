﻿using System;
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
            AutocompleteQueryParameters parameters = new AutocompleteQueryParameters()
            {
                Query = searchQuery,
                BrandEnabled = true,
                CategoryEnabled = false,
                ContentEnabled = false,
                ProductEnabled = false,
            };

            AutocompleteResult results = await GetAutocompleteResults(parameters);
            return results?.Brands;
        }

        public async Task<IList<AutocompleteProduct>> GetAutocompleteProducts(string searchQuery)
        {
            try
            {
                string url = AutocompleteUrl;
                List<string> parameters = new List<string>()
                {
                    "query=" + searchQuery,
                    "categoryEnabled=false",
                    "contentEnabled=false",
                    "productEnabled=true",
                    "brandEnabled=false",
                };

                url += "?" + string.Join("&", parameters);

                Autocomplete result = await GetAsyncWithCachedResponse<Autocomplete>(url);

                return result?.Products;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<AutocompleteResult> GetAutocompleteResults(AutocompleteQueryParameters parameters)
        {
            try
            {
                return await GetAsyncWithCachedResponse<AutocompleteResult>(AutocompleteUrl + parameters.ToQueryString());
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}