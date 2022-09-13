using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class VariantChildrenParameters : BaseQueryParameters
    {
        public string IncludeAttributes { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; }
    }
}
