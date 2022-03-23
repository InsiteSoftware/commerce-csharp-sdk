using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class QuoteLineService : ServiceBase, IQuoteLineService
    {
        public QuoteLineService(
            IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
            : base(optiAPIBaseServiceProvider)
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
                string url = string.Format(CommerceAPIConstants.QuoteLineUri, quoteId, quoteLineId);
                return await GetAsyncNoCache<QuoteLine>(url);
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
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

                string url = string.Format(CommerceAPIConstants.QuoteLineUri, quoteId, quoteLine.Id);
                return await PatchAsyncNoCache<QuoteLine>(url, stringContent);
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}