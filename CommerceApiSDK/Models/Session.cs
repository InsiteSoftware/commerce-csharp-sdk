using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models
{
    public static class FulfillmentMethodExtensions
    {
        public static string Description(this FulfillmentMethodType value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string DisplayName(this FulfillmentMethodType value)
        {
            FulfillmentMethodDisplayNameAttribute[] attributes =
                (FulfillmentMethodDisplayNameAttribute[])value
                    .GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(FulfillmentMethodDisplayNameAttribute), false);
            return attributes.Length > 0 ? attributes[0].DisplayName : string.Empty;
        }
    }

    public class FulfillmentMethodDisplayNameAttribute : Attribute
    {
        internal FulfillmentMethodDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }

    public class Session : BaseModel, ICloneable
    {
        /// <summary>Gets or sets a value indicating whether this instance is authenticated.</summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance has RFQ updates.</summary>
        public bool? HasRfqUpdates { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        public string UserName { get; set; }

        public string UserProfileId { get; set; }

        /// <summary>Gets or sets the user label.</summary>
        public string UserLabel { get; set; }

        /// <summary>Gets or sets the user roles.</summary>
        public string UserRoles { get; set; }

        /// <summary>Gets or sets the email.</summary>
        public string Email { get; set; }

        /// <summary>Gets or sets the password.</summary>
        public string Password { get; set; }

        /// <summary>Gets or sets the new password.</summary>
        public string NewPassword { get; set; }

        /// <summary>Gets or sets a value indicating whether this is a password reset request</summary>
        public bool ResetPassword { get; set; }

        /// <summary>Gets or sets a value indicating whether this is an account activation request</summary>
        public bool ActivateAccount { get; set; }

        /// <summary>Gets or sets the reset token.</summary>
        public string ResetToken { get; set; }

        /// <summary>Gets or sets a value indicating whether display change customer link.</summary>
        public bool DisplayChangeCustomerLink { get; set; }

        /// <summary>Gets or sets a value indicating whether redirect to change customer page on sign in.</summary>
        public bool RedirectToChangeCustomerPageOnSignIn { get; set; }

        /// <summary>Gets or sets the bill to.</summary>
        public BillTo BillTo { get; set; }

        /// <summary>Gets or sets the ship to.</summary>
        public ShipTo ShipTo { get; set; }

        /// <summary>Gets or sets the language.</summary>
        public Language Language { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        public Currency Currency { get; set; }

        /// <summary>Gets or sets the type of the device.</summary>
        public string DeviceType { get; set; }

        /// <summary>Gets or sets the persona.</summary>
        public string Persona { get; set; }

        /// <summary>Gets or sets the persona.</summary>
        public List<Persona> Personas { get; set; }

        /// <summary>Gets or sets the dashboard is homepage.</summary>
        public bool? DashboardIsHomepage { get; set; }

        /// <summary>Gets or sets a value indicating whether current user profile has salesperson</summary>
        public bool IsSalesPerson { get; set; }

        /// <summary>Gets or sets the custom landing page.</summary>
        public string CustomLandingPage { get; set; }

        /// <summary>Gets or sets a value indicating whether has default customer.</summary>
        public bool HasDefaultCustomer { get; set; }

        /// <summary>Gets or sets a value indicating whether remember me.</summary>
        public bool RememberMe { get; set; }

        /// <summary>Gets or sets a value indicating whether is restricted product removed from cart.</summary>
        public bool IsRestrictedProductRemovedFromCart { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>Gets or sets a value indicating whether customer was updated.</summary>
        public bool CustomerWasUpdated { get; set; }

        public bool IsGuest { get; set; }

        public string FulfillmentMethod { get; set; }

        public Warehouse PickUpWarehouse { get; set; }

        public object Clone()
        {
            string serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Session>(serialized);
        }

        [JsonIgnore]
        public bool IsRequisitioner
        {
            get
            {
                if (!string.IsNullOrEmpty(UserRoles))
                {
                    string[] roles = UserRoles.Split(
                        new string[] { "," },
                        StringSplitOptions.RemoveEmptyEntries
                    );
                    return roles.Any(
                        x => x.Trim().Equals("Requisitioner", StringComparison.OrdinalIgnoreCase)
                    );
                }

                return false;
            }
        }

        [JsonIgnore]
        public bool IsVMIUsers
        {
            get
            {
                if (!string.IsNullOrEmpty(UserRoles))
                {
                    string[] roles = UserRoles.Split(
                        new string[] { "," },
                        StringSplitOptions.RemoveEmptyEntries
                    );
                    return roles.Any(
                        x =>
                            x.Trim().Equals("VMI_Admin", StringComparison.OrdinalIgnoreCase)
                            || x.Trim().Equals("VMI_User", StringComparison.OrdinalIgnoreCase)
                    );
                }

                return false;
            }
        }
    }

    public class Persona : IEquatable<Persona>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool Equals(Persona other)
        {
            return IsDefault.Equals(other.IsDefault)
                && (
                    ReferenceEquals(Id, other.Id)
                    || (!string.IsNullOrEmpty(Id) && Id.Equals(other.Id))
                )
                && (
                    ReferenceEquals(Name, other.Name)
                    || (!string.IsNullOrEmpty(Name) && Name.Equals(other.Name))
                )
                && (
                    ReferenceEquals(Description, other.Description)
                    || (!string.IsNullOrEmpty(Description) && Description.Equals(other.Description))
                );
        }
    }
}
