using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which manages the user session
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// A local copy of user session. Do not change it without letting the server knows about that.
        /// </summary>
        Session CurrentSession { get; }

        Task<ServiceResponse<Session>> PostSession(Session session);

        Task<Session> PatchSession(Session session);

        Task<Session> GetCurrentSession();

        Task<HttpResponseMessage> DeleteCurrentSession();

        Task<Session> ResetPassword(string userName);

        /// <summary>
        /// Clear the cached data for all services
        /// </summary>
        void ClearCache();
    }
}