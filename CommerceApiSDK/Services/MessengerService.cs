using System;
using System.Collections.Concurrent;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;

namespace CommerceApiSDK.Services
{
    public sealed class OptiSubscriptionToken
    : IDisposable
    {
        public Guid Id { get; private set; }
        #pragma warning disable 414 // 414 is that this private field is only set, not used
        private readonly object[] _dependentObjects;
        #pragma warning restore 414
        private readonly Action _disposeMe;

        public OptiSubscriptionToken(Guid id, Action disposeMe, params object[] dependentObjects)
        {
            Id = id;
            _disposeMe = disposeMe;
            _dependentObjects = dependentObjects;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _disposeMe();
            }
        }
    }

    public class MessengerService : IMessengerService
    {
        private static readonly ConcurrentDictionary<
            Type,
            ConcurrentDictionary<Guid, BaseSubscription>
        > Subscriptions = new ConcurrentDictionary<
            Type,
            ConcurrentDictionary<Guid, BaseSubscription>
        >();

        public OptiSubscriptionToken OnSubscribe<TMessage>(Action<TMessage> action) where TMessage : OptiMessage
        {
            var subscriptionId = Subscribe(action);
            return new OptiSubscriptionToken(
                    subscriptionId,
                    () => Unsubscribe<TMessage>(subscriptionId),
                    action);
        }

        public Guid Subscribe<TMessage>(Action<TMessage> action) where TMessage : OptiMessage
        {
            var messageType = typeof(TMessage);
            if (!Subscriptions.TryGetValue(messageType, out var messageSubscriptions))
            {
                messageSubscriptions = new ConcurrentDictionary<Guid, BaseSubscription>();
                if (!Subscriptions.TryAdd(messageType, messageSubscriptions))
                {
                    throw new Exception(
                        $"Unable to add actions dictionary for {messageType.Name} type"
                    );
                }
            }

            var subscription = new WeakSubscription<TMessage>(new SimpleActionRunner(), action);

            if (!messageSubscriptions.TryAdd(subscription.Id, subscription))
            {
                throw new Exception(
                    $"Unable to add action to action dictionary for {messageType.Name} type"
                );
            }

            return subscription.Id;
        }

        public void Unsubscribe<TMessage>(Guid subscriptionId) where TMessage : OptiMessage
        {
            if (
                Subscriptions.TryGetValue(
                    typeof(TMessage),
                    out ConcurrentDictionary<Guid, BaseSubscription> messageSubscriptions
                ) && messageSubscriptions.ContainsKey(subscriptionId)
            )
            {
                messageSubscriptions.TryRemove(subscriptionId, out _);
            }
        }

        public void Publish(OptiMessage message)
        {
            var messageType = message.GetType();
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!Subscriptions.TryGetValue(messageType, out var messageSubscriptions))
            {
                return;
            }

            foreach (var subscription in messageSubscriptions.Values)
            {
                subscription.Invoke(message);
            }
        }

        private class WeakSubscription<TMessage> : TypedSubscription<TMessage>
            where TMessage : OptiMessage
        {
            private readonly WeakReference weakReference;

            public override bool IsAlive => weakReference.IsAlive;

            protected override bool TypedInvoke(TMessage message)
            {
                if (!weakReference.IsAlive)
                {
                    return false;
                }

                var action = weakReference.Target as Action<TMessage>;
                if (action == null)
                {
                    return false;
                }

                Call(
                    () =>
                    {
                        action?.Invoke(message);
                    }
                );
                return true;
            }

            public WeakSubscription(IActionRunner actionRunner, Action<TMessage> listener)
                : base(actionRunner)
            {
                weakReference = new WeakReference(listener);
            }
        }

        private abstract class TypedSubscription<TMessage> : BaseSubscription
            where TMessage : OptiMessage
        {
            protected TypedSubscription(IActionRunner actionRunner) : base(actionRunner) { }

            public sealed override bool Invoke(object message)
            {
                var typedMessage = message as TMessage;
                if (typedMessage == null)
                {
                    throw new Exception($"Unexpected message {message}");
                }

                return TypedInvoke(typedMessage);
            }

            protected abstract bool TypedInvoke(TMessage message);
        }

        private abstract class BaseSubscription
        {
            public Guid Id { get; private set; }

            public abstract bool IsAlive { get; }

            public abstract bool Invoke(object message);

            private readonly IActionRunner actionRunner;

            protected BaseSubscription(IActionRunner actionRunner)
            {
                this.actionRunner = actionRunner;
                Id = Guid.NewGuid();
            }

            protected void Call(Action action)
            {
                actionRunner.Run(action);
            }
        }

        private interface IActionRunner
        {
            void Run(Action action);
        }

        private class SimpleActionRunner : IActionRunner
        {
            public void Run(Action action)
            {
                action?.Invoke();
            }
        }
    }
}
