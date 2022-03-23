using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DashboardPanelsService : ServiceBase, IDashboardPanelsService
    {
        public DashboardPanelsService(IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
            : base(optiAPIBaseServiceProvider)
        {
        }

        public async Task<DashboardPanelsResult> GetDashboardPanelsAsync()
        {
            try
            {
                var url = CommerceAPIConstants.DashboardPanelUrl;
                return await GetAsyncNoCache<DashboardPanelsResult>(url);
            }
            catch(Exception ex)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(ex);
                return null;
            }
        }
    }
}
