using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services
{
    public class SessionService : ServiceBase, ISessionService
    {
        protected readonly IMessengerService optiMessenger;

        private Session currentSession;
        public Session CurrentSession => currentSession;

        public SessionService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            IMessengerService optiMessenger,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService,
                  loggerService)
        {
            this.optiMessenger = optiMessenger;
        }

        /// <summary>
        /// Deletes the current session stored locally and then sends a delete request to the server
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteCurrentSession()
        {
            currentSession = null;
            return await DeleteAsync(CommerceAPIConstants.CurrentSessionUri);
        }

        /// <summary>
        /// Creates a new session state on the server for the given session
        /// </summary>
        /// <param name="session">Session to be saved</param>
        /// <returns>Session result from the server</returns>
        /// <exception cref="Exception">Error when request fails</exception>
        public async Task<ServiceResponse<Session>> PostSession(Session session)
        {
            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(session));
                ServiceResponse<Session> result = await PostAsyncNoCacheWithErrorMessage<Session>(CommerceAPIConstants.PostSessionUri, stringContent);

                if (result?.Model != null)
                {
                    Client.StoreSessionState(result.Model);
                    currentSession = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        /// <summary>
        /// Updates the server's session object for the current signed in user
        /// </summary>
        /// <param name="session">Session to be updated</param>
        /// <returns>Session result from the server</returns>
        /// <exception cref="Exception">Error when request fails</exception>
        public async Task<Session> PatchSession(Session session)
        {
            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(session));
                Session result = await PatchAsyncNoCache<Session>(CommerceAPIConstants.CurrentSessionUri, stringContent);

                if (result != null)
                {
                    // If result != null then patch worked, but we have to call GetCurrentSession to get the most up
                    // to date version of the session
                    Session currentSession = await GetCurrentSession();
                }

                return currentSession;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        /// <summary>
        /// Sets the current session's state to reflect they are resetting their password
        /// </summary>
        /// <param name="userName">User to start password reset</param>
        /// <returns>Session result from the server</returns>
        /// <exception cref="Exception">Error when request fails</exception>
        public async Task<Session> ResetPassword(string userName)
        {
            try
            {
                Session session = new Session() { ResetPassword = true, UserName = userName };
                StringContent stringContent = await Task.Run(() => SerializeModel(session));

                return await PatchAsyncNoCache<Session>(CommerceAPIConstants.CurrentSessionUri, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        /// <summary>
        /// Clears all local caches
        /// </summary>
        public void ClearCache()
        {
            ClearAllCaches();
        }

        /// <summary>
        /// Sends a request to the server to get the latest session context
        /// </summary>
        /// <returns>Session result from the server</returns>
        /// <exception cref="Exception">Error when request fails</exception>
        public async Task<Session> GetCurrentSession()
        {
            try
            {
                Session result = await GetAsyncNoCache<Session>($"{CommerceAPIConstants.CurrentSessionUri}");

                if (result != null)
                {
                    if (currentSession != null)
                    {
                        if (!currentSession.Persona.Equals(result.Persona)
                            || !(currentSession.Personas != null && result.Personas != null && Enumerable.SequenceEqual(currentSession.Personas, result.Personas)))
                        {
                            optiMessenger.Publish(new SessionChangedOptiMessage());
                        }
                    }

                    Client.StoreSessionState(result);
                    currentSession = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}