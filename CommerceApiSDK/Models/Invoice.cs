using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class Invoice
    {
        public string BTAddress1 { get; set; }
        public string BTAddress2 { get; set; }
        public string BillToCity { get; set; }
        public string BTCompanyName { get; set; }
        public string BTCountry { get; set; }
        public string BillToPostalCode { get; set; }
        public string BillToState { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? CurrentBalance { get; set; }
        public string CurrentBalanceDisplay { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerPO { get; set; }
        public string CustomerSequence { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string DiscountAmountDisplay { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? Id { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public InvoiceLine[] InvoiceLines { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceTaxModel[] InvoiceHistoryTaxes { get; set; }
        public decimal? InvoiceTotal { get; set; }
        public string InvoiceTotalDisplay { get; set; }
        public string InvoiceType { get; set; }
        public bool? IsOpen { get; set; }
        public string Message { get; set; }
        public string Notes { get; set; }
        public string OrderTotalDisplay { get; set; }
        public decimal? OtherCharges { get; set; }
        public string OtherChargesDisplay { get; set; }
        public decimal? ProductTotal { get; set; }
        public string ProductTotalDisplay { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public string Salesperson { get; set; }
        public string ShipCode { get; set; }
        public decimal? ShippingAndHandling { get; set; }
        public string ShippingAndHandlingDisplay { get; set; }
        public string ShipViaDescription { get; set; }
        public string STAddress1 { get; set; }
        public string STAddress2 { get; set; }
        public string Status { get; set; }
        public string ShipToCity { get; set; }
        public string STCompanyName { get; set; }
        public string STCountry { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToState { get; set; }
        public bool Success { get; set; }
        public decimal? TaxAmount { get; set; }
        public string TaxAmountDisplay { get; set; }
        public string Terms { get; set; }
    }

    public class InvoiceLine
    {
        public string AltText { get; set; }
        public string CustomerName { get; set; }
        public string CustomerProductNumber { get; set; }
        public string Description { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string DiscountAmountDisplay { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string ErpOrderNumber { get; set; }
        public Guid? Id { get; set; }
        public decimal? LineNumber { get; set; }
        public string LinePOReference { get; set; }
        public decimal? LineTotal { get; set; }
        public string LineTotalDisplay { get; set; }
        public string LineType { get; set; }
        public string ManufacturerItem { get; set; }
        public string MediumImagePath { get; set; }
        public string Message { get; set; }
        public string Notes { get; set; }
        public string ProductErpNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductUri { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public decimal? QtyInvoiced { get; set; }
        public decimal? ReleaseNumber { get; set; }
        public string ShipmentNumber { get; set; }
        public string ShortDescription { get; set; }
        public bool Success { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public string UnitPriceDisplay { get; set; }
        public string Warehouse { get; set; }
        public Brand Brand { get; set; }
    }

    public class InvoiceTaxModel
    {
        public Guid? Id { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public int? SortOrder { get; set; }
        public bool Success { get; set; }
        public decimal? TaxAmount { get; set; }
        public string TaxAmountDisplay { get; set; }
        public string TaxCode { get; set; }
        public string TaxDescription { get; set; }
        public decimal? TaxRate { get; set; }
    }
}
