using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CommerceApiSDK.Extensions
{
    public static class HttpClientExtension
    {
        public static Task<HttpResponseMessage> GetAsync(
            this HttpClient httpClient,
            string requestUri,
            CancellationToken? cancellationToken = null
        )
        {
            if (httpClient == null)
            {
                return null;
            }

            return cancellationToken.HasValue
                ? httpClient.GetAsync(requestUri, cancellationToken.Value)
                : httpClient.GetAsync(requestUri);
        }
    }
}
