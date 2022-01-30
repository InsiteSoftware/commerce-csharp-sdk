namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class JobQuoteService : ServiceBase, IJobQuoteService
    {
        private const string JobQuoteUrl = "/api/v1/jobquotes";

        public JobQuoteService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<JobQuoteResult> GetJobQuotes()
        {
            try
            {
                return await this.GetAsyncNoCache<JobQuoteResult>(JobQuoteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
                var url = $"{JobQuoteUrl}/{jobQuoteId}";

                return await this.GetAsyncNoCache<JobQuoteDto>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
                var stringContent = await Task.Run(() => SerializeModel(jobQuoteUpdate));
                var url = $"{JobQuoteUrl}/{jobQuoteUpdate.JobQuoteId}";

                return await this.PatchAsyncNoCache<JobQuoteDto>(url, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}