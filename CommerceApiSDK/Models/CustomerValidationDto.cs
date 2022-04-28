namespace CommerceApiSDK.Models
{
    public class CustomerValidationDto
    {
        /// <summary>Gets or sets the first name.</summary>
        public FieldValidationDto FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        public FieldValidationDto LastName { get; set; }

        /// <summary>Gets or sets the company.</summary>
        public FieldValidationDto CompanyName { get; set; }

        public FieldValidationDto Attention { get; set; }

        /// <summary>Gets or sets the address1.</summary>
        public FieldValidationDto Address1 { get; set; }

        /// <summary>Gets or sets the address2.</summary>
        public FieldValidationDto Address2 { get; set; }

        public FieldValidationDto Address3 { get; set; }

        public FieldValidationDto Address4 { get; set; }

        /// <summary>Gets or sets the country.</summary>
        public FieldValidationDto Country { get; set; }

        /// <summary>Gets or sets the state.</summary>
        public FieldValidationDto State { get; set; }

        /// <summary>Gets or sets the city.</summary>
        public FieldValidationDto City { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        public FieldValidationDto PostalCode { get; set; }

        /// <summary>Gets or sets the phone.</summary>
        public FieldValidationDto Phone { get; set; }

        /// <summary>Gets or sets the email.</summary>
        public FieldValidationDto Email { get; set; }

        public FieldValidationDto Fax { get; set; }
    }

    public class FieldValidationDto
    {
        public bool IsRequired { get; set; }

        public bool IsDisabled { get; set; }

        public int? MaxLength { get; set; }
    }
}