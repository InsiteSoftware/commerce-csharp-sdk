using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class QuoteDto : Cart
    {
        public string QuoteLinesUri { get; set; }

        public string QuoteNumber { get; set; }

        /// <summary>Gets or sets the quote expiration date.</summary>
        public DateTime? ExpirationDate { get; set; }

        public string CustomerNumber { get; set; }

        public string CustomerName { get; set; }

        public string ShipToFullAddress { get; set; }

        public ICollection<QuoteLine> QuoteLineCollection { get; set; }

        public string UserName { get; set; }

        public bool IsEditable { get; set; }

        /// <summary>Gets or sets the message audits.</summary>
        public IList<QuoteMessage> MessageCollection { get; set; }

        public IList<CalculationMethod> CalculationMethods { get; set; }

        /// <summary>Gets or sets a value indicating whether its job quote.</summary>
        public bool IsJobQuote { get; set; }

        public string JobName { get; set; }
    }

    public class QuoteLine : CartLine
    {
        public PricingRfq PricingRfq { get; set; }
        public decimal? MaxQty { get; set; }
    }

    public class PricingRfq : BaseModel
    {
        public decimal? UnitCost { get; set; }
        public string UnitCostDisplay { get; set; }
        public decimal? ListPrice { get; set; }
        public string ListPriceDisplay { get; set; }
        public decimal? CustomerPrice { get; set; }
        public string CustomerPriceDisplay { get; set; }
        public decimal? MinimumPriceAllowed { get; set; }
        public string MinimumPriceAllowedDisplay { get; set; }
        public decimal? MaxDiscountPct { get; set; }
        public decimal? MinMarginAllowed { get; set; }
        public bool ShowListPrice { get; set; }
        public bool ShowCustomerPrice { get; set; }
        public bool ShowUnitCost { get; set; }
        public IList<BreakPrice> PriceBreaks { get; set; }
        public IList<CalculationMethod> CalculationMethods { get; set; }
        /// <summary>Gets or sets the validation messages.</summary>
        public List<KeyValuePair<string, string>> ValidationMessages { get; set; }
    }

    public class CalculationMethod
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string MaximumDiscount { get; set; }
        public string MinimumMargin { get; set; }
    }

    public class BreakPrice
    {
        public decimal? StartQty { get; set; }
        public string StartQtyDisplay { get; set; }
        public decimal? EndQty { get; set; }
        public string EndQtyDisplay { get; set; }
        public decimal? Price { get; set; }
        public string PriceDispaly { get; set; }
        public long? Percent { get; set; }
        public string CalculationMethod { get; set; }
    }

    public enum QuoteStatus
    {
        Cart,
        QuoteProposed,
        AwaitingApproval,
        QuoteRejected,
        QuoteRequested,
        QuoteCreated,
    }

    public enum QuoteType
    {
        Job,
        Quote,
    }
}
