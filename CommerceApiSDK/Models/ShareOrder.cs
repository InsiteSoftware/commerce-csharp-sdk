namespace CommerceApiSDK.Models
{
    public class ShareOrder : BaseModel
    {
        public string StEmail { get; set; }

        public string StPostalCode { get; set; }

        public string EmailTo { get; set; }

        public string EmailFrom { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string EntityId { get; set; }

        public string EntityName { get; set; }
    }
}

