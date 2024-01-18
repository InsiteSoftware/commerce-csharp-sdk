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
        public JobQuoteService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<JobQuoteResult>> GetJobQuotes()
        {
            try
            {
                return await GetAsyncNoCache<JobQuoteResult>(CommerceAPIConstants.JobQuoteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<JobQuoteResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<JobQuoteDto>> GetJobQuote(string jobQuoteId)
        {
            if (string.IsNullOrEmpty(jobQuoteId))
            {
                throw new ArgumentException($"{nameof(jobQuoteId)} is empty");
            }

            try
            {
                string url = $"{CommerceAPIConstants.JobQuoteUrl}/{jobQuoteId}";

                return await GetAsyncNoCache<JobQuoteDto>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<JobQuoteDto>(exception: exception);
            }
        }

        public async Task<ServiceResponse<JobQuoteDto>> UpdateJobQuote(
            JobQuoteUpdateParameter jobQuoteUpdate
        )
        {
            if (string.IsNullOrEmpty(jobQuoteUpdate?.JobQuoteId))
            {
                throw new ArgumentException("JobQuote or its Id is null or empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(jobQuoteUpdate));
                string url = $"{CommerceAPIConstants.JobQuoteUrl}/{jobQuoteUpdate.JobQuoteId}";

                return await PatchAsyncNoCache<JobQuoteDto>(url, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<JobQuoteDto>(exception: exception);
            }
        }
    }
}
