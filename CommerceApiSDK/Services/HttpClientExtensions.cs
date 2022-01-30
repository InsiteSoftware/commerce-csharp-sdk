namespace CommerceApiSDK.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

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
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent,
            };
            var response = new HttpResponseMessage();

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
