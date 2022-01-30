namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetOrderCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Order> Orders { get; set; }

        /// <summary>Gets or sets a value indicating whether [show erp order number].</summary>
        public bool ShowErpOrderNumber { get; set; }
    }
}
