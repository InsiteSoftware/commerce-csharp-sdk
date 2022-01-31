namespace CommerceApiSDK.Models.Parameters
{
    public class TranslationQueryParameters : BaseQueryParameters
    {
        public string Keyword { get; set; }

        public string Source { get; set; }

        public string LanguageCode { get; set; }
    }
}
