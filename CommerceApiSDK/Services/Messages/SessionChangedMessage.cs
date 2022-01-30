namespace CommerceApiSDK.Services.Messages
{
    using MvvmCross.Plugin.Messenger;

    public class SessionChangedMessage : MvxMessage
    {
        public SessionChangedMessage(object sender) : base(sender)
        {
        }
    }
}
