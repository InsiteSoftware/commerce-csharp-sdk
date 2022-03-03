using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DashboardPanelsService : ServiceBase, IDashboardPanelsService
    {
        private const string DashboardPanelUrl = "/api/v1/dashboardpanels";

        public DashboardPanelsService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<DashboardPanelsResult> GetDashboardpanelsAsync()
        {
            try
            {
                var url = DashboardPanelUrl;
                return await GetAsyncNoCache<DashboardPanelsResult>(url);
            }
            catch(Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }
    }
}
