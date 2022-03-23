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
            return URIMaxLength - (_optiAPIBaseServiceProvider.GetClientService().Url.AbsoluteUri.Length + CommerceAPIConstants.TranslationUrl.Length);
        }

        public TranslationService(
            IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
            : base(
                  optiAPIBaseServiceProvider)
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
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}
