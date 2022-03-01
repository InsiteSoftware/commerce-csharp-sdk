namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class BillToResult : BaseModel
    {
        public string ShipTosUri { get; set; }
        public bool IsGuest { get; set; }
        public string Label { get; set; }
        public string BudgetEnforcementLevel { get; set; }
        public string CostCodeTitle { get; set; }
        public string CustomerCurrencySymbol { get; set; }
        public List<CostCode> CostCodes { get; set; }
        public List<ShipTo> ShipTos { get; set; }
        public string Validation { get; set; }
        public bool IsDefault { get; set; }
        public AccountsReceivable AccountsReceivable { get; set; }
        public string Id { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerSequence { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactFullName { get; set; }
        public string CompanyName { get; set; }
        public string Attention { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string FullAddress { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public bool IsVmiLocation { get; set; }
    }

    public class AgingBucket
    {
        public int Amount { get; set; }
        public string AmountDisplay { get; set; }
        public string Label { get; set; }
    }

    public class AccountsReceivable
    {
        public List<AgingBucket> AgingBuckets { get; set; }
        public string AgingBucketTotal { get; set; }
        public string AgingBucketFuture { get; set; }
    }
   
}
