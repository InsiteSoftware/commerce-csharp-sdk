using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IAutocompleteService
    {
        Task<AutocompleteResult> GetAutocompleteResults(AutocompleteQueryParameters parameters);
        Task<IList<AutocompleteProduct>> GetAutocompleteProducts(string searchQuery);
        Task<IList<AutocompleteBrand>> GetAutocompleteBrands(string searchQuery);
    }
}
