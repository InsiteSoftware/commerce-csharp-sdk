using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services.Messages
{
    public class RefreshTokenExpiredMessage : MvxMessage
    {
        public RefreshTokenExpiredMessage(object sender) : base(sender)
        {
        }
    }
}
