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
        protected readonly IMessengerService OptiMessenger;

        private Session currentSession;
        public Session CurrentSession => currentSession;

        public SessionService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService,
            IMessengerService optiMessenger
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService)
        {
            this.OptiMessenger = optiMessenger;
        }

        /// <summary>
        /// Deletes the current session stored locally and then sends a delete request to the server
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteCurrentSession()
        {
            currentSession = null;
            return await DeleteAsync(CommerceAPIConstants.CurrentSessionUrl);
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
                ServiceResponse<Session> result = await PostAsyncNoCacheWithErrorMessage<Session>(
                    CommerceAPIConstants.PostSessionUrl,
                    stringContent
                );

                if (result?.Model != null)
                {
                    this.ClientService.StoreSessionState(result.Model);
                    currentSession = result.Model;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
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
                Session result = await PatchAsyncNoCache<Session>(
                    CommerceAPIConstants.CurrentSessionUrl,
                    stringContent
                );

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
                this.TrackingService.TrackException(exception);
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

                return await PatchAsyncNoCache<Session>(
                    CommerceAPIConstants.CurrentSessionUrl,
                    stringContent
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
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
                Session result = await GetAsyncNoCache<Session>(
                    $"{CommerceAPIConstants.CurrentSessionUrl}"
                );

                if (result != null)
                {
                    if (currentSession != null)
                    {
                        if (
                            !currentSession.Persona.Equals(result.Persona)
                            || !(
                                currentSession.Personas != null
                                && result.Personas != null
                                && Enumerable.SequenceEqual(
                                    currentSession.Personas,
                                    result.Personas
                                )
                            )
                        )
                        {
                            this.OptiMessenger.Publish(new SessionChangedOptiMessage());
                        }
                    }

                    this.ClientService.StoreSessionState(result);
                    currentSession = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
