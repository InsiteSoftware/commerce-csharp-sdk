using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IDashboardPanelsService
    {
        Task<ServiceResponse<DashboardPanelsResult>> GetDashboardPanelsAsync();
    }
}
