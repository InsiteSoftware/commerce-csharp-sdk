namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class AutocompleteResult : BaseModel
    {
        public IList<AutocompleteProduct> Products { get; set; }

        public IList<AutocompleteBrand> Brands { get; set; }

        public IList<AutocompleteCategory> Categories { get; set; }
    }
}
