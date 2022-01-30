namespace CommerceApiSDK.Models.Parameters
{
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class PaymentProfileQueryParameters : BaseQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
