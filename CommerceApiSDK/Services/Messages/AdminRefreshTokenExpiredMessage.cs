namespace CommerceApiSDK.Services.Messages
{
    using MvvmCross.Plugin.Messenger;

    public class AdminRefreshTokenExpiredMessage : MvxMessage
    {
        public AdminRefreshTokenExpiredMessage(object sender) : base(sender)
        {
        }
    }
}
