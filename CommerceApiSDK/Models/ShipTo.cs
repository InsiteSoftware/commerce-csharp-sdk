namespace CommerceApiSDK.Models
{
    public class ShipTo : Address
    {
        /// <summary>Indicates if the instance of the ShipTo is new</summary>
        public bool IsNew { get; set; }

        public bool OneTimeAddress { get; set; }

        /// <summary>Display label used in the ShipTo selection dropdown.  The BillTo, for example, will be returned as 'Use Billing Address' if the BillTo record is also marked as being a ShipTo.
        /// Otherwise it is a concatenation of the CompanySequence, CustomerName, City and State</summary>
        public string Label { get; set; }

        public CustomerValidationDto Validation { get; set; }

        /// <summary>Indicates if the ShipTo record is marked as the default customer/ShipTo for the current user</summary>
        public bool IsDefault { get; set; }
    }
}