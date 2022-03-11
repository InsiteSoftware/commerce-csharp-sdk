using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class BillTo : Address
    {
        /// <summary>URI to ShipTo collection, could also use expand parameter</summary>
        public string ShipTosUri { get; set; }

        /// <summary>A boolean value indicating whether the current customer record is processed as a guest which acts like an anonymous customer</summary>
        public bool IsGuest { get; set; }

        /// <summary>Display label used in the BillTo selection dropdown.  It is a concatenation of the CompanyNumber, CustomerName, City and State</summary>
        public string Label { get; set; }

        /// <summary>Budgets are enforced at either the Customer (across all ShipTos), specific ShipTo or None (no enforcement)</summary>
        public string BudgetEnforcementLevel { get; set; }

        /// <summary>Cost codes are defined by Customer and are used to assign a G/L code or other designator to specific purchases.  The title field is used to define what the code represents (i.e. Job #, Account #, Department)</summary>
        public string CostCodeTitle { get; set; }

        /// <summary>The currency symbol associated to their assigned currency</summary>
        public string CustomerCurrencySymbol { get; set; }

        /// <summary>Collection of cost codes defined for the customer</summary>
        public IList<CostCode> CostCodes { get; set; }

        /// <summary>ShipTo collection</summary>
        public IList<ShipTo> ShipTos { get; set; }

        /// <summary>Validation information for addresses based on the BillTo address setup.</summary>
        public CustomerValidationDto Validation { get; set; }

        /// <summary>Indicates if this BillTo is set as the user's default</summary>
        public bool IsDefault { get; set; }

        /// <summary>The accounts receivable information for the customer.</summary>
        public AccountsReceivable AccountsReceivable { get; set; }
    }

    public class CostCode
    {
        public Guid Id { get; set; }

        /// <summary>Cost code itself such as a General Ledger account number</summary>
        [JsonProperty("CostCode")]
        public string Code { get; set; }

        /// <summary>Description of the cost code</summary>
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}