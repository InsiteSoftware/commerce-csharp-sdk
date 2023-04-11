using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IAutocompleteService
    {
        Task<ServiceResponse<AutocompleteResult>> GetAutocompleteResults(AutocompleteQueryParameters parameters);
        Task<ServiceResponse<IList<AutocompleteProduct>>> GetAutocompleteProducts(string searchQuery);
        Task<ServiceResponse<IList<AutocompleteBrand>>> GetAutocompleteBrands(string searchQuery);
    }
}
