using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class AccountPaymentProfileCollectionResult : BaseModel
    {
        /// <summary>Gets or sets the account payment profile collection.</summary>
        public IList<AccountPaymentProfile> AccountPaymentProfiles { get; set; }

        /// <summary>Gets or sets the pagging.</summary>
        public Pagination Pagination { get; set; }
    }
}
