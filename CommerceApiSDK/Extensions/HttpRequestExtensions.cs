using System;
using System.Net.Http;

namespace CommerceApiSDK.Services
{
    public static class HttpRequestExtensions
    {
        private static string timeoutPropertyKey = "RequestTimeout";

        public static void SetTimeout(this HttpRequestMessage request, TimeSpan? timeout)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            request.Properties[timeoutPropertyKey] = timeout;
        }

        public static TimeSpan GetTimeout(this HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Properties.TryGetValue(
                    timeoutPropertyKey,
                    out object value)
                && value is TimeSpan timeout)
            {
                return timeout;
            }

            return ServiceBase.DefaultRequestTimeout;
        }
    }
}
