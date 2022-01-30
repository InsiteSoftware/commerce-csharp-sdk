﻿namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;

    /// <summary>
    ///     A service which fetches website settings
    /// </summary>
    public interface ISettingsService
    {
        Task<Settings> GetSettingsAsync();

        Task<ProductSettings> GetProductSettingsAsync();

        Task<AccountSettings> GetAccountSettingsAsync();

        Task<WebsiteSettings> GetWebsiteSettingsAsync();

        Task<WishListSettings> GetWishListSettingAsync();

        Task<CartSettings> GetCartSettingAsync();

        Task<MobileAppSettings> GetMobileAppSettingAsync();

        Task<QuoteSetting> GetQuoteSettingAsync();
    }
}