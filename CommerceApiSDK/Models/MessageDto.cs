namespace CommerceApiSDK.Models
{
    public class MessageDto : BaseModel
    {
        public string CustomerOrderId { get; set; }

        public string ToUserProfileId { get; set; }

        public string ToUserProfileName { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Process { get; set; }
    }
}
