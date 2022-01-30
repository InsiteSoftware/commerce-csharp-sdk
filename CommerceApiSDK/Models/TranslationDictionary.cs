namespace CommerceApiSDK.Models
{
    public class TranslationDictionary : BaseModel
    {
        public string Keyword { get; set; }

        public string Source { get; set; }

        public string Translation { get; set; }

        public string LanguageId { get; set; }

        public string LanguageCode { get; set; }
    }
}
