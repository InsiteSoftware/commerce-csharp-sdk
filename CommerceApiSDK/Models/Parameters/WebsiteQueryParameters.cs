using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;
using System.Collections.Generic;

namespace CommerceApiSDK.Models.Parameters
{
    public class WebsiteQueryParameters : BaseQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
