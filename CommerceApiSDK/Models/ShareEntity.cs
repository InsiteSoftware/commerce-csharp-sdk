namespace CommerceApiSDK.Models
{
    public class ShareEntity : BaseModel
    {
        public string EmailTo { get; set; }

        public string EmailFrom { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string EntityId { get; set; }

        public string EntityName { get; set; }
    }
}

