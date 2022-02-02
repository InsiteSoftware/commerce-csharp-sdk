using System;

namespace CommerceApiSDK.Models
{
    public class OrderPromotion
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        public decimal? Amount { get; set; }

        /// <summary>Gets or sets the amount display.</summary>
        public string AmountDisplay { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the order history line identifier.</summary>
        public virtual Guid? OrderHistoryLineId { get; set; }

        /// <summary>Gets or sets the promotion result type.</summary>
        public string PromotionResultType { get; set; }
    }
}
