using System.Threading.Tasks;
using CommerceApiSDK.Models.ContentManagement.Pages;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which fetches various page content management data.
    /// </summary>
    public interface IMobileContentService
    {
        /// <summary>
        ///     Load from server page content management data.
        /// </summary>
        /// <param name="pageName">
        ///     Used for specifying the desired page content management data (shop, search landing, etc...).
        /// </param>
        /// <param name="useCache">
        ///     Specify if this method might use cached responses.
        /// </param>
        /// <returns>Fetched PageContentManagement object.</returns>
        Task<ServiceResponse<PageContentManagement>> GetPageContenManagment(string pageName, bool useCache = true);

        /// <summary>
        ///     Load from server page content management data.
        /// </summary>
        /// <param name="pageName">
        ///     Used for specifying the desired page content management data (shop, search landing, etc...).
        /// </param>
        /// <param name="useCache">
        ///     Specify if this method might use cached responses.
        /// </param>
        /// <returns>Fetched page content management JSON string.</returns>
        Task<ServiceResponse<string>> GetPageContenManagmentString(string pageName, bool useCache = true);
    }
}
