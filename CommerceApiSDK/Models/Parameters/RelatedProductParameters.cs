using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class RelatedProductParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }

        public string Relationship { get; set; }
    }
}

