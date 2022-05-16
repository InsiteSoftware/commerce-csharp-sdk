using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListsQueryParameters : BaseQueryParameters
    {
        public string Query { get; set; }
    }
}
