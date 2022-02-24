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
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<MessageDto> AddMessage(MessageDto message)
        {
            if (message == null)
            {
                throw new ArgumentException("Message is empty");
            }

            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(message));
                return await PostAsyncNoCache<MessageDto>(MessageUri, stringContent);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}