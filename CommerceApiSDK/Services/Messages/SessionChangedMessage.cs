using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services.Messages
{
    public class SessionChangedMessage : MvxMessage
    {
        public SessionChangedMessage(object sender) : base(sender)
        {
        }
    }
}
