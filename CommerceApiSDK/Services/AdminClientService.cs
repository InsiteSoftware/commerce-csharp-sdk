using System.Net;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;
using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services
{
    public class AdminClientService : ClientService, IAdminClientService
    {
        protected override string ClientId { get; } = "isc_admin";
        protected override string ClientSecret { get; } = "F684FC94-B3BE-4BC7-B924-636561177C8F";

        protected override string BearerTokenStorageKey { get; } = "admin_bearerToken";
        protected override string RefreshTokenStorageKey { get; } = "admin_refreshToken";
        protected override string ExpiresInStorageKey { get; } = "admin_expiresIn";
        protected override string ApiScopeKey { get; } = "isc_admin_api";
        protected override string CookiesStorageKey { get; } = "admin_cookies";

        protected override string[] StoredCookiesNames { get; } = { "cms_CurrentContentModeSignature" };

        public Cookie CMSCurrentContentModeSignartureCookie
        {
            get
            {
                CookieCollection cookies = Cookies;
                if (cookies != null)
                {
                    foreach (Cookie cookie in Cookies)
                    {
                        if (cookie.Name == "cms_CurrentContentModeSignature")
                        {
                            return cookie;
                        }
                    }
                }

                return new Cookie("cms_CurrentContentModeSignature", string.Empty);
            }
        }

        public AdminClientService(
            ISecureStorageService secureStorageService,
            ILocalStorageService localStorageService,
            IMvxMessenger messenger,
            ITrackingService trackingService)
            : base(secureStorageService, localStorageService, messenger, trackingService)
        {
        }

        protected override void NotifyRefreshTokenExpired()
        {
            messenger.Publish(new AdminRefreshTokenExpiredMessage(this));
        }
    }
}
