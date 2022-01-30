namespace CommerceApiSDK.Services.Messages
{
    using MvvmCross.Plugin.Messenger;

    public class RefreshTokenExpiredMessage : MvxMessage
    {
        public RefreshTokenExpiredMessage(object sender) : base(sender)
        {
        }
    }
}
