using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services.Messages
{
    public class UserSignedOutMessage : MvxMessage
    {
        public bool IsRefreshTokenExpired;

        public UserSignedOutMessage(object sender) : base(sender)
        {
        }
    }
}