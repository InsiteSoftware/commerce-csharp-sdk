using System;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IMessengerService
    {
        IDisposable Subscribe<TMessage>(Action<TMessage> action) where TMessage : OptiMessage;

        void Publish(OptiMessage message);
    }
}
