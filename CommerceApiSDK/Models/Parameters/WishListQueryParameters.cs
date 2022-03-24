using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListQueryParameters : BaseQueryParameters
    {
        /// <summary>
        /// Options: sharedusers, staticlist, hiddenproducts, getalllines, schedule
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; }

        /// <summary>
        /// Options: excludelistlines, listlines
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Exclude { get; set; }
    }
}
