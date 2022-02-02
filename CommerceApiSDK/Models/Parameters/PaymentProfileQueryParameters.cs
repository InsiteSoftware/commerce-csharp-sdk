using System.Collections.Generic;
using CommerceApiSDK.Attributes;

namespace CommerceApiSDK.Models.Parameters
{
    public class PaymentProfileQueryParameters : BaseQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
