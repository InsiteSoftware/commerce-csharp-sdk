using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class TranslationService : ServiceBase, ITranslationService
    {
        public static int URIMaxLength = 2048;

        public int GetMaxLengthOfTranslationText()
        {
            return URIMaxLength - (Client.Url.AbsoluteUri.Length + CommerceAPIConstants.TranslationUrl.Length);
        }

        public TranslationService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService,
                  loggerService)
        {
        }

        public async Task<TranslationResults> GetTranslations(TranslationQueryParameters parameters = null)
        {
            try
            {
                string url = CommerceAPIConstants.TranslationUrl;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                TranslationResults translationResult = await GetAsyncNoCache<TranslationResults>(url);

                return translationResult;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
