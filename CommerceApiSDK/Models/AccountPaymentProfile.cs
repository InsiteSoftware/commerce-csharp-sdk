using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class AccountPaymentProfile : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the card type.</summary>
        public string CardType { get; set; }

        /// <summary>Gets or sets the expiration date month/year.</summary>
        public string ExpirationDate { get; set; }

        /// <summary>Gets or sets the masked card number.</summary>
        public string MaskedCardNumber { get; set; }

        /// <summary>Gets or sets the card identifier.</summary>
        public string CardIdentifier { get; set; }

        /// <summary>Gets or sets the card holder name.</summary>
        public string CardHolderName { get; set; }

        /// <summary>Gets or sets the address1.</summary>
        public string Address1 { get; set; }

        /// <summary>Gets or sets the address2.</summary>
        public string Address2 { get; set; }

        /// <summary>Gets or sets the address3.</summary>
        public string Address3 { get; set; }

        /// <summary>Gets or sets the address4.</summary>
        public string Address4 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        public string City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        public string State { get; set; }

        /// <summary>Gets or sets the postcode.</summary>
        public string PostalCode { get; set; }

        /// <summary>Gets or sets the country.</summary>
        public string Country { get; set; }

        /// <summary>Gets or sets a value indicating whether gets or sets the is payment profile default.</summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the token scheme.</summary>
        [JsonIgnore]
        public string TokenScheme { get; set; }

        /// <summary>
        /// Return 4 letters ending of card number.
        /// </summary>
        [JsonIgnore]
        public string CardNumberEnding
        {
            get
            {
                int length = 4;
                if (string.IsNullOrEmpty(MaskedCardNumber))
                {
                    return string.Empty;
                }

                if (MaskedCardNumber.Length <= length)
                {
                    return MaskedCardNumber;
                }

                return MaskedCardNumber.Substring(MaskedCardNumber.Length - length, length);
            }
        }

        [JsonIgnore]
        /// <summary>
        /// Return month of expiration.
        /// </summary>
        public int ExpirationMonth
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ExpirationDate))
                {
                    return 0;
                }

                string[] separator = { "/" };
                string[] expiration = ExpirationDate.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(expiration[0], out int result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        [JsonIgnore]
        /// <summary>
        /// Return month of expiration.
        /// </summary>
        public int ExpirationYear
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ExpirationDate))
                {
                    return 0;
                }

                string[] separator = { "/" };
                string[] expiration = ExpirationDate.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (expiration.Length <= 1)
                {
                    return 0;
                }

                if (int.TryParse(expiration[1], out int result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        [JsonIgnore]
        public bool IsExpired
        {
            get
            {
                int yearTwoLetter = DateTime.Today.Year % 100;
                if (ExpirationYear < yearTwoLetter)
                {
                    return true;
                }

                if (ExpirationYear > yearTwoLetter)
                {
                    return false;
                }

                // Current year
                if (ExpirationMonth < DateTime.Today.Month)
                {
                    return true;
                }

                return false;
            }
        }

        [JsonIgnore]
        public string SecurityCode;
    }

    public class AccountPaymentProfileCollection : BaseModel
    {
        /// <summary>Gets or sets the account payment profile collection.</summary>
        public IList<AccountPaymentProfile> AccountPaymentProfiles { get; set; }

        /// <summary>Gets or sets the pagging.</summary>
        public Pagination Pagination { get; set; }
    }
}
