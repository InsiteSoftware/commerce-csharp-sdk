using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IRealTimePricingService
    {
        Task<GetRealTimePricingResult> GetProductRealTimePrices(
            RealTimePricingParameters parameters
        );
    }
}
