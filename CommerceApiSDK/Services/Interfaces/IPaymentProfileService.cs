using System;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which manages the account payment profile.
    /// </summary>
    public interface IPaymentProfileService
    {
        Task<AccountPaymentProfileCollection> GetPaymentProfiles(PaymentProfileQueryParameters parameters = null);
        Task<AccountPaymentProfile> GetPaymentProfile(Guid accountPaymentProfileId);
        Task<ServiceResponse<AccountPaymentProfile>> SavePaymentProfile(AccountPaymentProfile accountPaymentProfile);
        Task<bool> DeletePaymentProfile(Guid accountPaymentProfileId);
        Task<Cart> GetPaymentCurrentCart(PaymentProfileQueryParameters parameters = null);
    }
}
