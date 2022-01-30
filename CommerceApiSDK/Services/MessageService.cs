namespace CommerceApiSDK.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;

    public class MessageService : ServiceBase, IMessageService
    {
        private const string MessageUri = "/api/v1/messages";

        public MessageService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
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
                var stringContent = await Task.Run(() => SerializeModel(message));
                return await this.PostAsyncNoCache<MessageDto>(MessageUri, stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}