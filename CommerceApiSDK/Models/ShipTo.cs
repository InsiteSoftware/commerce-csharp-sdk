namespace CommerceApiSDK.Models
{
    public class ShipTo : Address
    {
        /// <summary>Indicates if the instance of the ShipTo is new</summary>
        public bool IsNew { get; set; }

        public bool OneTimeAddress { get; set; }

        public CustomerValidationDto Validation { get; set; }

        /// <summary>Indicates if the ShipTo record is marked as the default customer/ShipTo for the current user</summary>
        public bool IsDefault { get; set; }
    }
}
