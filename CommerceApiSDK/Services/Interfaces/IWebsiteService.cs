namespace CommerceApiSDK.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Results;

    public interface IWebsiteService
    {
        Task<Website> GetWebsite();
        Task<bool> HasWebsiteCache();
        Task<WebsiteCrosssells> GetWebsiteCrosssells();
        Task<bool> HasWebsiteCrosssellsCache();
        Task<GetSiteMessageCollectionResult> GetSiteMessages(List<string> names = null);
        Task<string> GetSiteMessage(string messageName, string defaultMessage = null);
        Task<LanguageCollectionModel> GetLanguages();

        /// <summary>
        /// Get full URL for path with domain, access token and customer ids
        /// </summary>
        /// <returns></returns>
        Task<string> GetAuthorizedURL(string path);

        Task<IList<Country>> GetCountries();
    }
}