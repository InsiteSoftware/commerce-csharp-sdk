using System;

namespace CommerceApiSDK.Models
{
    public class ProductDetail
    {
        public string Name { get; set; }

        public string ModelNumber { get; set; }

        public string Sku { get; set; }

        public string UpcCode { get; set; }

        public string Unspsc { get; set; }

        public string ProductCode { get; set; }

        public string PriceCode { get; set; }

        public int SortOrder { get; set; }

        public int MultipleSaleQty { get; set; }

        public bool CanBackOrder { get; set; }

        public string RoundingRule { get; set; }

        public Guid? ReplacementProductId { get; set; }

        public bool IsHazardousGood { get; set; }

        public bool HasMsds { get; set; }

        public bool IsSpecialOrder { get; set; }

        public bool IsGiftCard { get; set; }

        public bool AllowAnyGiftCardAmount { get; set; }

        public string TaxCode1 { get; set; }

        public string TaxCode2 { get; set; }

        public string TaxCategory { get; set; }

        public Guid? VatCodeId { get; set; }

        public string ShippingClassification { get; set; }

        public decimal ShippingLength { get; set; }

        public decimal ShippingWidth { get; set; }

        public decimal ShippingHeight { get; set; }

        public decimal ShippingWeight { get; set; }

        public LegacyConfiguration Configuration { get; set; }
    }
}

