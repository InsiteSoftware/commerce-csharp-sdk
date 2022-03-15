using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class TranslationResults : BaseModel
    {
        public IList<TranslationDictionary> TranslationDictionaries { get; set; }

        public Pagination Pagination { get; set; }
    }
}
