﻿using System;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IMessengerService
    {
        OptiSubscriptionToken Subscribe<TMessage>(Action<TMessage> action) where TMessage : OptiMessage;
        
        void Unsubscribe<TMessage>(Guid subscriptionId) where TMessage : OptiMessage;

        void Publish(OptiMessage message);
    }
}
