using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class AutocompleteResult : BaseModel
    {
        public IList<AutocompleteProduct> Products { get; set; }

        public IList<AutocompleteBrand> Brands { get; set; }

        public IList<AutocompleteCategory> Categories { get; set; }
    }
}
