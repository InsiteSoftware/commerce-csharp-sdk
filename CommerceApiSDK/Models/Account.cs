using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class Account : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the email.</summary>
        public string Email { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        public string UserName { get; set; }

        /// <summary>Gets or sets the password.</summary>
        public string Password { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is subscribed.</summary>
        public bool? IsSubscribed { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is guest.</summary>
        public bool IsGuest { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can approve orders.</summary>
        public bool CanApproveOrders { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can view approval orders.</summary>
        public bool CanViewApprovalOrders { get; set; }

        /// <summary>Gets or sets the bill to id.  Returned from creating a new account to return the new bill to id.</summary>
        public Guid? BillToId { get; set; }

        /// <summary>Gets or sets the ship to id.  Returned from creating a new account to return the new ship to id.</summary>
        public Guid? ShipToId { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the role.</summary>
        public string Role { get; set; }

        /// <summary>Gets or sets the approver.</summary>
        public string Approver { get; set; }

        /// <summary>Gets or sets a value indicating whether is approved.</summary>
        public bool? IsApproved { get; set; }

        /// <summary>Gets or sets the activation status</summary>
        public string ActivationStatus { get; set; }

        /// <summary>Gets or sets the default location.</summary>
        public string DefaultLocation { get; set; }

        /// <summary>Gets or sets a last login on.</summary>
        public DateTime? LastLoginOn { get; set; }

        /// <summary>Gets or sets the available approvers.</summary>
        public IList<string> AvailableApprovers { get; set; }

        /// <summary>Gets or sets the available roles.</summary>
        public IList<string> AvailableRoles { get; set; }

        /// <summary>Gets or sets a value indicating whether this account requires activation via email</summary>
        [JsonIgnore]
        public bool? RequiresActivation { get; set; }

        /// <summary>Gets or sets a value indicating whether set default customer.</summary>
        public bool SetDefaultCustomer { get; set; }

        /// <summary>Gets or sets the default customer id.</summary>
        public string DefaultCustomerId { get; set; }

        /// <summary>Gets or sets the default fulfillment method.</summary>
        public string DefaultFulfillmentMethod { get; set; }

        /// <summary>Gets or sets the default warehouse.</summary>
        public Warehouse DefaultWarehouse { get; set; }

        /// <summary>Gets or sets the default warehouse id.</summary>
        public string DefaultWarehouseId { get; set; }
    }
}
