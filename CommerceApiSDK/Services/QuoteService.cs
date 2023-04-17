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
        public QuoteService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<QuoteResult>> GetQuotes(QuoteQueryParameters parameters)
        {
            try
            {
                string url =
                    $"{CommerceAPIConstants.QuoteUrl}{parameters?.ToQueryString() ?? string.Empty}";

                return await GetAsyncNoCache<QuoteResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> GetQuote(string quoteId)
        {
            if (string.IsNullOrEmpty(quoteId))
            {
                throw new ArgumentException($"{nameof(quoteId)} is empty");
            }

            try
            {
                string url = $"{CommerceAPIConstants.QuoteUrl}/{quoteId}";

                return await GetAsyncNoCache<QuoteDto>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> SaveQuote(QuoteDto quote)
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
                    return await PostAsyncNoCache<QuoteDto>(
                        CommerceAPIConstants.QuoteUrl,
                        stringContent
                    );
                }
                else
                {
                    string editQuoteUrl = $"{CommerceAPIConstants.QuoteUrl}/{quote.Id}";
                    return await PatchAsyncNoCache<QuoteDto>(editQuoteUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> RequestQuote(RequesteAQuoteParameters param)
        {
            if (param == null)
            {
                throw new ArgumentException("Paramester is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                return await PostAsyncNoCache<QuoteDto>(
                    CommerceAPIConstants.QuoteUrl,
                    stringContent
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> RequestQuote(SalesRepRequesteAQuoteParameters param)
        {
            if (param == null)
            {
                throw new ArgumentException("Paramester is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                return await PostAsyncNoCache<QuoteDto>(
                    CommerceAPIConstants.QuoteUrl,
                    stringContent
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto> (exception: exception);
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
                string url = $"{CommerceAPIConstants.QuoteUrl}/{quoteId}";
                HttpResponseMessage deleteResponse = await DeleteAsync(url);
                return deleteResponse != null && deleteResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<ServiceResponse<QuoteMessage>> PostQuoteMessage(string quoteId, QuoteMessage message)
        {
            if (string.IsNullOrEmpty(quoteId) || message == null)
            {
                throw new ArgumentException($"{nameof(quoteId)} or message is null/empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(message));

                string messageQuote = $"{CommerceAPIConstants.QuoteUrl}/{quoteId}";
                return await PostAsyncNoCache<QuoteMessage>(messageQuote, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteMessage>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteLine>> PatchQuoteLine(string quoteId, QuoteLine quoteLine)
        {
            if (string.IsNullOrEmpty(quoteId) || quoteLine == null)
            {
                throw new ArgumentException($"{nameof(quoteId)} or message is null/empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(quoteLine));
                string messageQuote =
                    $"{CommerceAPIConstants.QuoteUrl}/{quoteId}/quotelines/{quoteLine.Id}";
                return await PatchAsyncNoCache<QuoteLine>(messageQuote, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteLine>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> SubmitQuote(QuoteDto quote)
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

                StringContent stringContent = await Task.Run(
                    () => SerializeModel(quoteRequestParameter)
                );
                if (string.IsNullOrEmpty(quoteRequestParameter.QuoteId))
                {
                    return await PostAsyncNoCache<QuoteDto>(
                        CommerceAPIConstants.QuoteUrl,
                        stringContent
                    );
                }
                else
                {
                    string editQuoteUrl =
                        $"{CommerceAPIConstants.QuoteUrl}/{quoteRequestParameter.QuoteId}";
                    return await PatchAsyncNoCache<QuoteDto>(editQuoteUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception);
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
                string editQuoteUrl = $"{CommerceAPIConstants.QuoteUrl}/{param.QuoteId}";
                return await PatchAsyncNoCacheWithErrorMessage<QuoteDto>(
                    editQuoteUrl,
                    stringContent
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteDto>> QuoteLinePricing(
            string quoteId,
            QuoteLinePricingQueryParameters param
        )
        {
            if (string.IsNullOrEmpty(quoteId) || param == null || param.Id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Quote or quote id is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(param));
                string editUrl = $"{CommerceAPIConstants.QuoteUrl}/{quoteId}/quotelines/{param.Id}";
                return await PatchAsyncNoCache<QuoteDto>(editUrl, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteDto>(exception: exception) ;
            }
        }

        public async Task<ServiceResponse<QuoteLine>> GetQuoteLine(string quoteId, string quoteLineId)
        {
            if (string.IsNullOrEmpty(quoteId) || string.IsNullOrEmpty(quoteLineId))
            {
                throw new ArgumentException($"QuoteId or QuoteLineId or QuoteLine is empty/null");
            }

            try
            {
                string url = string.Format(CommerceAPIConstants.QuoteLineUrl, quoteId, quoteLineId);
                return await GetAsyncNoCache<QuoteLine>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteLine>(exception: exception);
            }
        }

        public async Task<ServiceResponse<QuoteLine>> UpdateQuoteLine(string quoteId, QuoteLine quoteLine)
        {
            if (
                string.IsNullOrEmpty(quoteId)
                || quoteLine == null
                || quoteLine.Id.Equals(Guid.Empty)
            )
            {
                throw new ArgumentException($"QuoteId or QuoteLineId or QuoteLine is empty/null");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(quoteLine));

                string url = string.Format(
                    CommerceAPIConstants.QuoteLineUrl,
                    quoteId,
                    quoteLine.Id
                );
                return await PatchAsyncNoCache<QuoteLine>(url, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<QuoteLine>(exception: exception);
            }
        }
    }
}
