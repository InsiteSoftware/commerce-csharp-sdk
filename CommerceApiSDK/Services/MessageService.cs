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
            ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
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

                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}