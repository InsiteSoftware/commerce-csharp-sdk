using System;

namespace CommerceApiSDK.Models.Parameters
{
    public class VariantChildrenDetailParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }

        public Guid? VariantChildId { get; set; }
    }
}
