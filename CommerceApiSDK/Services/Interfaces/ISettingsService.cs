using System.Threading.Tasks;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    ///     A service which fetches website settings
    /// </summary>
    public interface ISettingsService
    {
        Task<ServiceResponse<Settings>> GetSettingsAsync();

        Task<ServiceResponse<ProductSettings>> GetProductSettingsAsync();

        Task<ServiceResponse<AccountSettings>> GetAccountSettingsAsync();

        Task<ServiceResponse<WebsiteSettings>> GetWebsiteSettingsAsync();

        Task<ServiceResponse<WishListSettings>> GetWishListSettingAsync();

        Task<ServiceResponse<CartSettings>> GetCartSettingAsync();

        Task<ServiceResponse<MobileAppSettings>> GetMobileAppSettingAsync();

        Task<ServiceResponse<QuoteSettings>> GetQuoteSettingAsync();
    }
}
