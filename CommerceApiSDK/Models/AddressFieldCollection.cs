namespace CommerceApiSDK.Models
{
    public class AddressFieldCollection : BaseModel
    {
        public AddressFieldDisplayCollection BillToAddressFields { get; set; }

        public AddressFieldDisplayCollection ShipToAddressFields { get; set; }
    }
}
