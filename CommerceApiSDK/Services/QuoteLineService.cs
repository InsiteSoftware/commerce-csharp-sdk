namespace CommerceApiSDK.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;

    public class QuoteLineService : ServiceBase, IQuoteLineService
    {
        private const string QuoteLineUri = "/api/v1/quotes/{0}/quotelines/{1}";

        public QuoteLineService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<QuoteLine> GetQuoteLine(string quoteId, string quoteLineId)
        {
            if (string.IsNullOrEmpty(quoteId) || string.IsNullOrEmpty(quoteLineId))
            {
                throw new ArgumentException($"QuoteId or QuoteLineId or QuoteLine is empty/null");
            }

            try
            {
                string url = string.Format(QuoteLineUri, quoteId, quoteLineId);
                return await GetAsyncNoCache<QuoteLine>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteLine> UpdateQuoteLine(string quoteId, QuoteLine quoteLine)
        {
            if (string.IsNullOrEmpty(quoteId) || quoteLine == null || quoteLine.Id.Equals(Guid.Empty))
            {
                throw new ArgumentException($"QuoteId or QuoteLineId or QuoteLine is empty/null");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(quoteLine));

                string url = string.Format(QuoteLineUri, quoteId, quoteLine.Id);
                return await PatchAsyncNoCache<QuoteLine>(url, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}