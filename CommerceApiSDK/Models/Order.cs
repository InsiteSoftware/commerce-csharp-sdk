using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class Order
    {
        /// <summary>Gets or sets the order history identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the erp order number.</summary>
        public string ErpOrderNumber { get; set; }

        /// <summary>Gets or sets the web order number.</summary>
        public string WebOrderNumber { get; set; }

        /// <summary>Gets or sets the order date.</summary>
        public DateTime OrderDate { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public string Status { get; set; }

        /// <summary>Gets or sets the status display.</summary>
        public string StatusDisplay { get; set; }

        /// <summary>Gets or sets the customer number.</summary>
        public string CustomerNumber { get; set; }

        /// <summary>Gets or sets the customer sequence.</summary>
        public string CustomerSequence { get; set; }

        /// <summary>Gets or sets the customer po.</summary>
        public string CustomerPO { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        public string CurrencyCode { get; set; }

        /// <summary>Gets or sets the currency symbol.</summary>
        public string CurrencySymbol { get; set; }

        /// <summary>Gets or sets the terms.</summary>
        public string Terms { get; set; }

        /// <summary>Gets or sets the ship code.</summary>
        public string ShipCode { get; set; }

        /// <summary>Gets or sets the salesperson.</summary>
        public string Salesperson { get; set; }

        /// <summary>Gets or sets the name of the BT company.</summary>
        public string BTCompanyName { get; set; }

        /// <summary>Gets or sets the BT address1.</summary>
        public string BTAddress1 { get; set; }

        /// <summary>Gets or sets the BT address2.</summary>
        public string BTAddress2 { get; set; }

        /// <summary>Gets or sets the bill to city.</summary>
        public string BillToCity { get; set; }

        /// <summary>Gets or sets the state of the bill to.</summary>
        public string BillToState { get; set; }

        /// <summary>Gets or sets the bill to postal code.</summary>
        public string BillToPostalCode { get; set; }

        /// <summary>Gets or sets the BT country.</summary>
        public string BTCountry { get; set; }

        /// <summary>Gets or sets the name of the st company.</summary>
        public string STCompanyName { get; set; }

        /// <summary>Gets or sets the st address1.</summary>
        public string STAddress1 { get; set; }

        /// <summary>Gets or sets the st address2.</summary>
        public string STAddress2 { get; set; }

        public string STAddress3 { get; set; }

        public string STAddress4 { get; set; }

        /// <summary>Gets or sets the ship to city.</summary>
        public string ShipToCity { get; set; }

        /// <summary>Gets or sets the state of the ship to.</summary>
        public string ShipToState { get; set; }

        /// <summary>Gets or sets the ship to postal code.</summary>
        public string ShipToPostalCode { get; set; }

        /// <summary>Gets or sets the st country.</summary>
        public string STCountry { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        public string Notes { get; set; }

        /// <summary>Gets or sets the product total.</summary>
        public decimal ProductTotal { get; set; }

        /// <summary>Gets or sets the order sub total.</summary>
        public decimal OrderSubTotal { get; set; }

        /// <summary>Gets or sets the order discount amount.</summary>
        public decimal OrderDiscountAmount { get; set; }

        /// <summary>Gets or sets the product discount amount.</summary>
        public decimal ProductDiscountAmount { get; set; }

        public decimal ShippingAndHandling { get; set; }

        /// <summary>Gets the shipping charges.</summary>
        public decimal ShippingCharges { get; set; }

        /// <summary>Gets the handling charges.</summary>
        public decimal HandlingCharges { get; set; }

        /// <summary>Gets or sets the other charges.</summary>
        public decimal OtherCharges { get; set; }

        /// <summary>Gets or sets the tax amount.</summary>
        public decimal TaxAmount { get; set; }

        /// <summary>Gets or sets the order total.</summary>
        public decimal OrderTotal { get; set; }

        /// <summary>Gets or sets the modify date.</summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>Gets or sets the requested delivery date.</summary>
        public DateTime? RequestedDeliveryDateDisplay { get; set; }

        /// <summary>Gets or sets the order history lines.</summary>
        public IList<OrderLine> OrderLines { get; set; }

        /// <summary>Gets or sets the order history promotions.</summary>
        public IList<OrderPromotion> OrderPromotions { get; set; }

        /// <summary>Gets or sets the shipment packages.</summary>
        public IList<ShipmentPackageDto> ShipmentPackages { get; set; }

        /// <summary>Gets or sets the return reason codes.</summary>
        public IList<string> ReturnReasons { get; set; }

        /// <summary>Gets or sets the order history taxes.</summary>
        public IList<OrderHistoryTaxDto> OrderHistoryTaxes { get; set; }

        /// <summary>Gets or sets the product total display.</summary>
        public string ProductTotalDisplay { get; set; }

        /// <summary>Gets or sets the order sub total display.</summary>
        public string OrderSubTotalDisplay { get; set; }

        /// <summary>Gets or sets the order total including product total, taxes, shipping and handling, discounts, and other charges. </summary>
        public string OrderGrandTotalDisplay { get; set; }

        /// <summary>Gets or sets the order discount amount display.</summary>
        public string OrderDiscountAmountDisplay { get; set; }

        /// <summary>Gets or sets the product discount amount display.</summary>
        public string ProductDiscountAmountDisplay { get; set; }

        public string TaxAmountDisplay { get; set; }

        /// <summary>Gets or sets the formatted display of the order tax amount.</summary>
        public string TotalTaxDisplay { get; set; }

        /// <summary>Gets or sets the shipping and handling display.</summary>
        public string ShippingAndHandlingDisplay { get; set; }

        /// <summary>Gets or sets the shipping charges display.</summary>
        public string ShippingChargesDisplay { get; set; }

        /// <summary>Gets or sets the handling charges display.</summary>
        public string HandlingChargesDisplay { get; set; }

        /// <summary>Gets or sets the other charges display.</summary>
        public string OtherChargesDisplay { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can add to cart.</summary>
        public bool CanAddToCart { get; set; }

        /// <summary>Gets or sets a value indicating whether all line items can be added to a cart.</summary>
        public bool CanAddAllToCart { get; set; }

        /// <summary>Indicates whether or not to display taxes and shipping and handling charges where this order appears in the user interface.</summary>
        public bool ShowTaxAndShipping { get; set; }

        public string ShipViaDescription { get; set; }

        public string FulfillmentMethod { get; set; }

        public string VmiLocationId { get; set; }

        public string VmiLocationName { get; set; }

        [JsonIgnore]
        public bool? ShowWebOrderNumber { get; set; }

        [JsonIgnore]
        public bool? ShowPoNumber { get; set; }

        [JsonIgnore]
        public bool? ShowTermsCode { get; set; }

        [JsonIgnore]
        public string OrderNumberLabel { get; set; }

        [JsonIgnore]
        public string WebOrderNumberLabel { get; set; }

        [JsonIgnore]
        public string PONumberLabel { get; set; }

        [JsonIgnore]
        public string OrderNumber
        {
            get
            {
                return string.IsNullOrEmpty(this.ErpOrderNumber) ? this.WebOrderNumber : this.ErpOrderNumber;
            }
        }
    }

    public class OrderHistoryTaxDto
    {
        /// <summary>Gets or sets the tax code.</summary>
        public string TaxCode { get; set; }

        /// <summary>Gets or sets the tax description.</summary>
        public string TaxDescription { get; set; }

        /// <summary>Gets or sets the tax rate.</summary>
        public decimal TaxRate { get; set; }

        /// <summary>Gets or sets the tax amount.</summary>
        public decimal TaxAmount { get; set; }

        /// <summary>Gets or sets the tax amount display.</summary>
        public string TaxAmountDisplay { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        public int SortOrder { get; set; }
    }

    public class ShipmentPackageDto
    {
        /// <summary>Gets or sets the id.</summary>
        public virtual Guid Id { get; set; }

        /// <summary>Gets or sets the shipment date.</summary>
        public virtual DateTime ShipmentDate { get; set; }

        /// <summary>Gets or sets the carrier.</summary>
        public virtual string Carrier { get; set; }

        /// <summary>Gets or sets the ship via.</summary>
        public virtual string ShipVia { get; set; }

        /// <summary>Gets or sets the tracking url.</summary>
        public virtual string TrackingUrl { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        public virtual string TrackingNumber { get; set; }

        /// <summary>Gets or sets the pack slip.</summary>
        public virtual string PackSlip { get; set; }

        [JsonIgnore]
        public string TrackButtonTitle { get; set; }

        [JsonIgnore]
        public string ShipDateTitle { get; set; }
    }

    public class OrderStatusMapping : BaseModel
    {
        /// <summary>Gets or sets the id.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the erp order status.</summary>
        public string ErpOrderStatus { get; set; }

        /// <summary>Gets or sets the display name.</summary>
        public string DisplayName { get; set; }

        /// <summary>Gets or sets a value indicating whether is default.</summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets a value indicating whether allow rma.</summary>
        public bool AllowRma { get; set; }

        /// <summary>Gets or sets a value indicating whether allow cancellation.</summary>
        public bool AllowCancellation { get; set; }
    }
}