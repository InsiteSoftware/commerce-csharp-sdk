using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class CountriesQueryParameters : BaseQueryParameters
    {
        /// <summary>
        /// Here are parameters to be passed in the Expand List.
        /// Options: states
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}

