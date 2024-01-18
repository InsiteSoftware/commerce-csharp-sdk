using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DashboardPanelsService : ServiceBase, IDashboardPanelsService
    {
        public DashboardPanelsService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        )
            : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<DashboardPanelsResult>> GetDashboardPanelsAsync()
        {
            try
            {
                var url = CommerceAPIConstants.DashboardPanelUrl;
                return await GetAsyncNoCache<DashboardPanelsResult>(url);
            }
            catch (Exception ex)
            {
                this.TrackingService.TrackException(ex);
                return GetServiceResponse<DashboardPanelsResult>(exception: ex);
            }
        }
    }
}
