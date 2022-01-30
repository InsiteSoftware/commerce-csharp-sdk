namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class TranslationService : ServiceBase, ITranslationService
    {
        private const string TranslationUrl = "/api/v1/translationdictionaries";

        public static int URIMaxLength = 2048;

        public int GetMaxLengthOfTranslationText()
        {
            return URIMaxLength - (base.Client.Url.AbsoluteUri.Length + TranslationUrl.Length);
        }

        public TranslationService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService)
        {
        }

        public async Task<TranslationResults> GetTranslations(TranslationQueryParameters parameters = null)
        {
            try
            {
                var url = TranslationUrl;

                if (parameters != null)
                {
                    var queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var translationResult = await this.GetAsyncNoCache<TranslationResults>(url);

                return translationResult;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
