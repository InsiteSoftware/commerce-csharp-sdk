namespace CommerceApiSDK.Services.Messages
{
    using MvvmCross.Plugin.Messenger;

    public class UserSignedOutMessage : MvxMessage
    {
        public bool IsRefreshTokenExpired;

        public UserSignedOutMessage(object sender) : base(sender)
        {
        }
    }
}