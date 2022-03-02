using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DashboardpanelsService : ServiceBase, IDashboardpanelsService
    {
        private const string DashboardpanelUrl = "/api/v1/dashboardpanels";

        public DashboardpanelsService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<DashboardpanelsResult> GetDashboardpanelsAsync()
        {
            try
            {
                var url = DashboardpanelUrl;
                return await GetAsyncNoCache<DashboardpanelsResult>(url);
            }
            catch(Exception ex)
            {
                TrackingService.TrackException(ex);
                return null;
            }
        }
    }
}
