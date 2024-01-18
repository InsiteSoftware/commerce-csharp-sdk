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
            return URIMaxLength
                - (
                    this.ClientService.Url.AbsoluteUri.Length
                    + CommerceAPIConstants.TranslationUrl.Length
                );
        }

        public TranslationService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<TranslationResults>> GetTranslations(
            TranslationQueryParameters parameters = null
        )
        {
            try
            {
                string url = CommerceAPIConstants.TranslationUrl;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var response = await GetAsyncNoCache<TranslationResults>(url);

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<TranslationResults>(exception: exception);
            }
        }
    }
}
