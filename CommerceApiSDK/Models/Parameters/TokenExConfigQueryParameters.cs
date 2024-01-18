namespace CommerceApiSDK.Models.Parameters
{
    public class TokenExConfigQueryParameters : BaseQueryParameters
    {
        public string Token { get; set; }

        public string Origin { get; set; }

        public bool IsECheck { get; set; }
    }
}
