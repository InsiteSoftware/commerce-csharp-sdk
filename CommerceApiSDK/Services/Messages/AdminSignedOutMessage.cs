using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services.Messages
{
    public class AdminSignedOutMessage : MvxMessage
    {
        public bool IsRefreshTokenExpired;

        public AdminSignedOutMessage(object sender) : base(sender)
        {
        }
    }
}