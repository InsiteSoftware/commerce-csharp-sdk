namespace CommerceApiSDK.Models
{
    public class Availability : BaseModel
    {
        public int MessageType { get; set; }

        public string Message { get; set; }

        public bool RequiresRealTimeInventory { get; set; }
    }
}
