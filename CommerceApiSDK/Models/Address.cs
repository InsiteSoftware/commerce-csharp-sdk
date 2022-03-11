namespace CommerceApiSDK.Models
{
    public class Address : BaseModel
    {
        /// <summary>Identifier</summary>
        public string Id { get; set; }

        /// <summary>CustomerNumber along with CustomerSequence uniquely defines a customer record and may be different than the ERPNumber</summary>
        public string CustomerNumber { get; set; }

        /// <summary>CustomerSequence is normally blank for the BillTo and has some value to define each ShipTo record</summary>
        public string CustomerSequence { get; set; }

        /// <summary>CustomerName is derived from the CompanyName + LastName + FirstName fields</summary>
        public string CustomerName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>CompanyName is used as the first line of the address</summary>
        public string CompanyName { get; set; }

        public string Attention { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public State State { get; set; }

        public Country Country { get; set; }

        public string Phone { get; set; }

        /// <summary>Concatenates the address fields, city, state, and postal code but not country</summary>
        public string FullAddress { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string IsVmiLocation { get; set; }
    }
}