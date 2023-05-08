using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class MessageService : ServiceBase, IMessageService
    {
        private const string MessageUri = "/api/v1/messages";

        public MessageService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        public async Task<ServiceResponse<MessageDto>> AddMessage(MessageDto message)
        {
            if (message == null)
            {
                throw new ArgumentException("Message is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(message));
                return await PostAsyncNoCacheWithErrorMessage<MessageDto>(MessageUri, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
