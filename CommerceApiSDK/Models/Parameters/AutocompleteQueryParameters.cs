namespace CommerceApiSDK.Models.Parameters
{
    public class AutocompleteQueryParameters : BaseQueryParameters
    {
        public string Query { get; set; }
        public bool CategoryEnabled { get; set; }
        public bool ContentEnabled { get; set; }
        public bool ProductEnabled { get; set; }
        public bool BrandEnabled { get; set; }
    }
}
