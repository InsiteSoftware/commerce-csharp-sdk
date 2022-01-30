namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class TranslationResults : BaseModel
    {
        public IList<TranslationDictionary> TranslationDictionaries { get; set; }

        public Pagination Pagination { get; set; }
    }
}
