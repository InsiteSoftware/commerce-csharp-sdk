namespace CommerceApiSDK.Services.Messages
{
    using MvvmCross.Plugin.Messenger;

    public class AdminSignedOutMessage : MvxMessage
    {
        public bool IsRefreshTokenExpired;

        public AdminSignedOutMessage(object sender) : base(sender)
        {
        }
    }
}