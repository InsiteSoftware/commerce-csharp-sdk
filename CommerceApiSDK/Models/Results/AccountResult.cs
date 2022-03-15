using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class AccountResult : BaseModel
    {
        public IList<Account> Accounts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
