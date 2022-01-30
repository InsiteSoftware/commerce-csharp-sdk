namespace CommerceApiSDK.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Services.Messages;
    using MvvmCross.Plugin.Messenger;

    public class SessionService : ServiceBase, ISessionService
    {
        protected readonly IMvxMessenger messenger;
        private const string PostSessionUri = "/api/v1/sessions";
        private const string CurrentSessionUri = "/api/v1/sessions/current";

        private Session currentSession;
        public Session CurrentSession => this.currentSession;

        public SessionService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            IMvxMessenger messenger,
            ICacheService cacheService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService)
        {
            this.messenger = messenger;
        }

        /// <summary>
        /// Deletes the current session stored locally and then sends a delete request to the server
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteCurrentSession()
        {
            this.currentSession = null;
            return await this.DeleteAsync(CurrentSessionUri);
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
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(session));
                var result = await this.PostAsyncNoCacheWithErrorMessage<Session>(PostSessionUri, stringContent);

                if (result?.Model != null)
                {
                    this.Client.StoreSessionState(result.Model);
                    this.currentSession = result.Model;
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
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(session));
                var result = await this.PatchAsyncNoCache<Session>(CurrentSessionUri, stringContent);

                if (result != null)
                {
                    // If result != null then patch worked, but we have to call GetCurrentSession to get the most up
                    // to date version of the session
                    var currentSession = await this.GetCurrentSession();
                }

                return this.currentSession;
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
                var session = new Session() { ResetPassword = true, UserName = userName };
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(session));

                return await this.PatchAsyncNoCache<Session>(CurrentSessionUri, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        /// <summary>
        /// Clears all local caches
        /// </summary>
        public void ClearCache()
        {
            this.ClearAllCaches();
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
                var result = await this.GetAsyncNoCache<Session>($"{CurrentSessionUri}");

                if (result != null)
                {
                    if (this.currentSession != null)
                    {
                        if (!this.currentSession.Persona.Equals(result.Persona)
                            || !(this.currentSession.Personas != null && result.Personas != null && Enumerable.SequenceEqual(this.currentSession.Personas, result.Personas)))
                        {
                            this.messenger.Publish(new SessionChangedMessage(this));
                        }
                    }

                    this.Client.StoreSessionState(result);
                    this.currentSession = result;
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