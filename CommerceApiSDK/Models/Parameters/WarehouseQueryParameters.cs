using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class WarehouseQueryParameters : BaseQueryParameters
	{
		/// <summary>Options: alternatewarehouses, properties</summary>
		[QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
		public List<string> Expand { get; set; } = null;
	}
}

