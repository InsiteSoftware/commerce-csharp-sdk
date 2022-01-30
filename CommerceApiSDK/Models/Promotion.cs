namespace CommerceApiSDK.Models
{
    using System;
    using System.Collections.Generic;

    public class AddPromotion : BaseModel
    {
        public string PromotionCode { get; set; }
    }

    public class PromotionCollectionModel : AddPromotion
    {
        public IList<Promotion> Promotions { get; set; }
    }

    public class Promotion : BaseModel
    {
        /// <summary>Gets or sets the id.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the promotion code.</summary>
        public string PromotionCode { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        public decimal Amount { get; set; }

        /// <summary>Gets or sets the amount display.</summary>
        public string AmountDisplay { get; set; }

        /// <summary>Gets or sets a value indicating whether promotion applied.</summary>
        public bool PromotionApplied { get; set; }

        /// <summary>Gets or sets the message.</summary>
        public string Message { get; set; }

        /// <summary>Gets or sets the order line id.</summary>
        public virtual Guid? OrderLineId { get; set; }

        /// <summary>Gets or sets the promotion result type.</summary>
        public string PromotionResultType { get; set; }
    }
}
