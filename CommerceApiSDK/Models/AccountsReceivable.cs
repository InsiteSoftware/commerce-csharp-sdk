using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class AccountsReceivable
    {
        public List<AgingBucket> AgingBuckets { get; set; }

        public AgingBucket AgingBucketTotal { get; set; }

        public AgingBucket AgingBucketFuture { get; set; }
    }
}
