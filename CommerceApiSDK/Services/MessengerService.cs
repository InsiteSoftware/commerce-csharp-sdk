using System;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services
{
    public class MessengerService : IMessengerService
    {
        public MessengerService()
        {
        }

        public void Publish(OptiMessage message)
        {
            
        }

        public IDisposable Subscribe<TMessage>(Action<TMessage> action) where TMessage : OptiMessage
        {
            return null;
        }
    }
}
