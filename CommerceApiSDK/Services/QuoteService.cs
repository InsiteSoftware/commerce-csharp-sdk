using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class QuoteService : ServiceBase, IQuoteService
    {
        private const string QuoteUri = "/api/v1/quotes";

        public QuoteService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<QuoteResult> GetQuotes(QuoteQueryParameters parameters)
        {
            try
            {
                string url = $"{QuoteUri}{parameters?.ToQueryString() ?? string.Empty}";

                return await GetAsyncNoCache<QuoteResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> GetQuote(string quoteId)
        {
            if (string.IsNullOrEmpty(quoteId))
            {
                throw new ArgumentException($"{nameof(quoteId)} is empty");
            }

            try
            {
                string url = $"{QuoteUri}/{quoteId}";

                return await GetAsyncNoCache<QuoteDto>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> SaveQuote(QuoteDto quote)
        {
            if (quote == null)
            {
                throw new ArgumentException("Quote is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(quote));
                if (string.IsNullOrEmpty(quote.QuoteNumber))
                {
                    return await PostAsyncNoCache<QuoteDto>(QuoteUri, stringContent);
                }
                else
                {
                    string editQuoteUrl = $"{QuoteUri}/{quote.Id}";
                    return await PatchAsyncNoCache<QuoteDto>(editQuoteUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> RequestQuote(RequesteAQuoteParameters param)
        {
            if (param == null)
            {
                throw new ArgumentException("Paramester is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                return await PostAsyncNoCache<QuoteDto>(QuoteUri, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> RequestQuote(SalesRepRequesteAQuoteParameters param)
        {
            if (param == null)
            {
                throw new ArgumentException("Paramester is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                return await PostAsyncNoCache<QuoteDto>(QuoteUri, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> DeleteQuote(string quoteId)
        {
            if (string.IsNullOrEmpty(quoteId))
            {
                throw new ArgumentException($"{nameof(quoteId)} is empty");
            }

            try
            {
                string url = $"{QuoteUri}/{quoteId}";
                HttpResponseMessage deleteResponse = await DeleteAsync(url);
                return deleteResponse != null && deleteResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<QuoteMessage> PostQuoteMessage(string quoteId, QuoteMessage message)
        {
            if (string.IsNullOrEmpty(quoteId) || message == null)
            {
                throw new ArgumentException($"{nameof(quoteId)} or message is null/empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(message));

                string messageQuote = $"{QuoteUri}/{quoteId}";
                return await PostAsyncNoCache<QuoteMessage>(messageQuote, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteLine> PatchQuoteLine(string quoteId, QuoteLine quoteLine)
        {
            if (string.IsNullOrEmpty(quoteId) || quoteLine == null)
            {
                throw new ArgumentException($"{nameof(quoteId)} or message is null/empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(quoteLine));
                string messageQuote = $"{QuoteUri}/{quoteId}/quotelines/{quoteLine.Id}";
                return await PatchAsyncNoCache<QuoteLine>(messageQuote, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> SubmitQuote(QuoteDto quote)
        {
            if (quote == null || quote.Id == null)
            {
                throw new ArgumentException("Quote or quote id is empty");
            }

            try
            {
                QuoteRequestedParameter quoteRequestParameter = new QuoteRequestedParameter()
                {
                    QuoteId = quote.Id,
                    Status = quote.Status,
                    ExpirationDate = quote.ExpirationDate,
                };

                StringContent stringContent = await Task.Run(() => SerializeModel(quoteRequestParameter));
                if (string.IsNullOrEmpty(quoteRequestParameter.QuoteId))
                {
                    return await PostAsyncNoCache<QuoteDto>(QuoteUri, stringContent);
                }
                else
                {
                    string editQuoteUrl = $"{QuoteUri}/{quoteRequestParameter.QuoteId}";
                    return await PatchAsyncNoCache<QuoteDto>(editQuoteUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<QuoteDto>> QuoteAll(QuoteAllQueryParameters param)
        {
            if (param == null || param.QuoteId.Equals(Guid.Empty))
            {
                throw new ArgumentException("Quote or quote id is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                string editQuoteUrl = $"{QuoteUri}/{param.QuoteId}";
                return await PatchAsyncNoCacheWithErrorMessage<QuoteDto>(editQuoteUrl, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<QuoteDto> QuoteLinePricing(string quoteId, QuoteLinePricingQueryParameters param)
        {
            if (string.IsNullOrEmpty(quoteId) || param == null || param.Id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Quote or quote id is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                string editUrl = $"{QuoteUri}/{quoteId}/quotelines/{param.Id}";
                return await PatchAsyncNoCache<QuoteDto>(editUrl, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}