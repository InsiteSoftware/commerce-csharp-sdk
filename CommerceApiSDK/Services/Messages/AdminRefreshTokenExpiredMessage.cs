using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services.Messages
{
    public class AdminRefreshTokenExpiredMessage : MvxMessage
    {
        public AdminRefreshTokenExpiredMessage(object sender) : base(sender)
        {
        }
    }
}
