namespace CommerceApiSDK.Models.Results
{
    using System;
    using System.Collections.Generic;

    public class AccountResult : BaseModel
    {
        public IList<Account> Accounts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
