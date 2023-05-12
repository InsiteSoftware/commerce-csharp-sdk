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

        Task<ServiceResponse<Session>> PatchSession(Session session);

        Task<Session> GetCurrentSession();

        Task<HttpResponseMessage> DeleteCurrentSession();

        Task<ServiceResponse<Session>> ResetPassword(string userName);
    }
}
