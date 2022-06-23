namespace CommerceApiSDK.Models
{
    public class BreakPriceDto
    {
        public decimal BreakQty { get; set; }

        public string BreakQtyDisplay => BreakQty.ToString("0.####");

        public decimal BreakPrice { get; set; }

        public string BreakPriceDisplay { get; set; }

        public string SavingsMessage { get; set; }

        public decimal BreakPriceWithVat { get; set; }

        public string BreakPriceWithVatDisplay { get; set; }
    }
}
