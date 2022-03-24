using System;
namespace CommerceApiSDK.Models
{
    public static class ClientConfig
    {
        public static string HostUrl;
        public static string ClientId;
        public static string ClientSecret;
        public static bool IsCachingEnabled = true;

        public static void InitClientConfig(string hostURL, string clientId, string clientSecret, bool isCachingEnabled)
        {
            HostUrl = hostURL;
            ClientId = clientId;
            ClientSecret = clientSecret;
            IsCachingEnabled = isCachingEnabled;
        }
    }
}