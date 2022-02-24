using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class JobQuoteService : ServiceBase, IJobQuoteService
    {
        private const string JobQuoteUrl = "/api/v1/jobquotes";

        public JobQuoteService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<JobQuoteResult> GetJobQuotes()
        {
            try
            {
                return await GetAsyncNoCache<JobQuoteResult>(JobQuoteUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<JobQuoteDto> GetJobQuote(string jobQuoteId)
        {
            if (string.IsNullOrEmpty(jobQuoteId))
            {
                throw new ArgumentException($"{nameof(jobQuoteId)} is empty");
            }

            try
            {
                string url = $"{JobQuoteUrl}/{jobQuoteId}";

                return await GetAsyncNoCache<JobQuoteDto>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<JobQuoteDto> UpdateJobQuote(JobQuoteUpdateParameter jobQuoteUpdate)
        {
            if (string.IsNullOrEmpty(jobQuoteUpdate?.JobQuoteId))
            {
                throw new ArgumentException("JobQuote or its Id is null or empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(jobQuoteUpdate));
                string url = $"{JobQuoteUrl}/{jobQuoteUpdate.JobQuoteId}";

                return await PatchAsyncNoCache<JobQuoteDto>(url, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}