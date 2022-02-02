using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Patch async extension method
        /// </summary>
        /// <param name="client">The http client.</param>
        /// <param name="requestUri">Uri to fetch.</param>
        /// <param name="iContent">Body content.</param>
        /// <returns>A response message.</returns>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
        {
            HttpMethod method = new HttpMethod("PATCH");
            HttpRequestMessage request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent,
            };
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
            }

            return response;
        }
    }
}
