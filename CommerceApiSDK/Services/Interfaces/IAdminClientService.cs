using System.Net;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IAdminClientService : IClientService
    {
        /// <summary>
        /// Cookie for proving that we are authorized to view the current CMS content mode data.
        /// </summary>
        Cookie CMSCurrentContentModeSignartureCookie { get; }
    }
}
