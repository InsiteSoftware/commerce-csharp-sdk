namespace CommerceApiSDK.Models
{
    public class BreakPriceDto
    {
        public decimal BreakQty { get; set; }

        public string BreakQtyDisplay => BreakQty.ToString("0.####");

        /// <summary>Gets or sets the break price.</summary>
        public decimal BreakPrice { get; set; }

        /// <summary>Gets or sets the break price display.</summary>
        public string BreakPriceDisplay { get; set; }

        /// <summary>Gets or sets the savings message.</summary>
        public string SavingsMessage { get; set; }
    }
}
