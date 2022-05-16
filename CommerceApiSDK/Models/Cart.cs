using System;
using System.Collections.Generic;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models
{
    public class Cart : BaseModel
    {
        /// <summary>Gets or sets the cart lines URI.</summary>
        public string CartLinesUri { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public string Status { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public string StatusDisplay { get; set; }

        /// <summary>Gets or sets the type.</summary>
        public string Type { get; set; }

        /// <summary>Gets or sets the type display.</summary>
        public string TypeDisplay { get; set; }

        /// <summary>Gets or sets the order number.</summary>
        public string OrderNumber { get; set; }

        /// <summary>Gets or sets the erp order number.</summary>
        public string ErpOrderNumber { get; set; }

        /// <summary>Gets or sets the order date.</summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>Gets or sets the bill to.</summary>
        public BillTo BillTo { get; set; }

        /// <summary>Gets or sets the ship to.</summary>
        public ShipTo ShipTo { get; set; }

        /// <summary>Gets or sets the user label.</summary>
        public string UserLabel { get; set; }

        /// <summary>Gets or sets the user roles.</summary>
        public string UserRoles { get; set; }

        /// <summary>Gets or sets the ship to label.</summary>
        public string ShipToLabel { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        public string Notes { get; set; }

        /// <summary>Gets or sets the carrier.</summary>
        public CarrierDto Carrier { get; set; }

        /// <summary>Gets or sets the ship via.</summary>
        public ShipViaDto ShipVia { get; set; }

        /// <summary>Gets or sets the payment method.</summary>
        public PaymentMethodDto PaymentMethod { get; set; }

        /// <summary>Gets or sets the po number.</summary>
        public string PoNumber { get; set; }

        /// <summary>Gets or sets the promotion code.</summary>
        public string PromotionCode { get; set; }

        /// <summary>Gets or sets the name of the initiated by user.</summary>
        public string InitiatedByUserName { get; set; }

        /// <summary>Gets or sets the total quantity ordered.</summary>
        public int TotalQtyOrdered { get; set; }

        /// <summary>Gets or sets the line count.</summary>
        public int LineCount { get; set; }

        /// <summary>Gets or sets the total count display.</summary>
        public int TotalCountDisplay { get; set; }

        /// <summary>Gets or sets the quote required count.</summary>
        public int QuoteRequiredCount { get; set; }

        /// <summary>Gets or sets the order sub total.</summary>
        public decimal OrderSubTotal { get; set; }

        /// <summary>Gets or sets the order sub total display.</summary>
        public string OrderSubTotalDisplay { get; set; }

        /// <summary>Gets or sets the order sub total with out product discounts.</summary>
        public decimal OrderSubTotalWithOutProductDiscounts { get; set; }

        /// <summary>Gets or sets the order sub total with out product discounts display.</summary>
        public string OrderSubTotalWithOutProductDiscountsDisplay { get; set; }

        /// <summary>Gets or sets the total tax.</summary>
        public decimal TotalTax { get; set; }

        /// <summary>Gets or sets the total tax display.</summary>
        public string TotalTaxDisplay { get; set; }

        /// <summary>Gets or sets the shipping and handling.</summary>
        public decimal ShippingAndHandling { get; set; }

        /// <summary>Gets or sets the shipping and handling display.</summary>
        public string ShippingAndHandlingDisplay { get; set; }

        /// <summary>Gets or sets the shipping charges display.</summary>
        public string ShippingChargesDisplay { get; set; }

        /// <summary>Gets or sets the handling charges display.</summary>
        public string HandlingChargesDisplay { get; set; }

        /// <summary>Gets or sets the other charges display.</summary>
        public string OtherChargesDisplay { get; set; }

        /// <summary>Gets or sets the order grand total.</summary>
        public decimal OrderGrandTotal { get; set; }

        /// <summary>Gets or sets the order grand total display.</summary>
        public string OrderGrandTotalDisplay { get; set; }

        /// <summary>Gets or sets the cost code label.</summary>
        public string CostCodeLabel { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is authenticated.</summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is sales person.</summary>
        public bool IsSalesperson { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is subscribed.</summary>
        public bool IsSubscribed { get; set; }

        /// <summary>Gets or sets a value indicating whether [requires po number].</summary>
        public bool RequiresPoNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can shop.</summary>
        public bool DisplayContinueShoppingLink { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can modify order.</summary>
        public bool CanModifyOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can save order.</summary>
        public bool CanSaveOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can bypass checkout address.</summary>
        public bool CanBypassCheckoutAddress { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can requisition.</summary>
        public bool CanRequisition { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can request quote.</summary>
        public bool CanRequestQuote { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can edit cost code.</summary>
        public bool CanEditCostCode { get; set; }

        /// <summary>Gets or sets a value indicating whether [show tax and shipping].</summary>
        public bool ShowTaxAndShipping { get; set; }

        /// <summary>Gets or sets a value indicating whether [show line notes].</summary>
        public bool ShowLineNotes { get; set; }

        /// <summary>Gets or sets a value indicating whether [show cost code].</summary>
        public bool ShowCostCode { get; set; }

        /// <summary>Gets or sets a value indicating whether [show subscription in footer].</summary>
        public bool ShowNewsletterSignup { get; set; }

        /// <summary>Gets or sets a value indicating whether [show po number].</summary>
        public bool ShowPoNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether [show credit card].</summary>
        public bool ShowCreditCard { get; set; }

        /// <summary>Gets or sets a value indicating whether [show pay pal].</summary>
        public bool ShowPayPal { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is awaiting approval.</summary>
        public bool IsAwaitingApproval { get; set; }

        /// <summary>Gets or sets a value indicating whether [requires approval].</summary>
        public bool RequiresApproval { get; set; }

        /// <summary>Gets or sets the approver reason.</summary>
        public string ApproverReason { get; set; }

        /// <summary>Gets or sets a value indicating whether the user has an assigned approver.</summary>
        public bool HasApprover { get; set; }

        /// <summary>Gets or sets the name of the primary salesperson.</summary>
        public string SalespersonName { get; set; }

        /// <summary>Gets or sets the payment options.</summary>
        public PaymentOptionsDto PaymentOptions { get; set; }

        /// <summary>Gets or sets the cost codes.</summary>
        public IList<CostCodeDto> CostCodes { get; set; }

        /// <summary>Gets or sets the carriers.</summary>
        public IList<CarrierDto> Carriers { get; set; }

        /// <summary>Gets or sets the cart lines.</summary>
        public IList<CartLine> CartLines { get; set; }

        /// <summary>Gets or sets the customer order taxes.</summary>
        public IList<CustomerOrderTaxDto> CustomerOrderTaxes { get; set; }

        /// <summary>Gets or sets the can check out.</summary>
        public bool CanCheckOut { get; set; }

        /// <summary>Gets or sets whether cart has insufficient inventory.</summary>
        public bool HasInsufficientInventory { get; set; }

        /// <summary>Gets or sets the currency symbol.</summary>
        public virtual string CurrencySymbol { get; set; }

        /// <summary>Gets or sets a value delivery date with date time offset formated string. For example "2019-02-04T11:13:19-06:00".</summary>
        public string RequestedDeliveryDate { get; set; }

        /// <summary>Gets the current delivery date displayed in correct format defined by your session context.</summary>
        public DateTime? RequestedDeliveryDateDisplay { get; }

        /// <summary>Gets or sets a value delivery date with date time offset formated string. For example "2019-02-04T11:13:19-06:00" .</summary>
        public string RequestedPickUpDate { get; set; }

        /// <summary>Gets the current pick up date displayed in correct format defined by your session context.</summary>
        public DateTime? RequestedPickUpDateDisplay { get; }

        /// <summary>Gets or sets the cart not priced.</summary>
        public virtual bool CartNotPriced { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        public IList<string> Messages { get; set; }
    }

    public class CarrierDto
    {
        /// <summary>Gets or sets the carrier id.</summary>
        public Guid? Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the collection of <see cref="ShipViaDto" />.</summary>
        public IList<ShipViaDto> ShipVias { get; set; }
    }

    public class ShipViaDto
    {
        /// <summary>Gets or sets the ship via identifier.</summary>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets a value indicating whether is default.</summary>
        public bool IsDefault { get; set; }
    }

    public class PaymentMethodDto
    {
        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is credit card.</summary>
        public bool IsCreditCard { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is payment profile.</summary>
        public bool IsPaymentProfile { get; set; }
    }

    public class PaymentOptionsDto
    {
        /// <summary>Gets or sets the payment terms.</summary>
        public IList<PaymentMethodDto> PaymentMethods { get; set; }

        /// <summary>Gets or sets the card types.</summary>
        public IList<KeyValuePair<string, string>> CardTypes { get; set; }

        /// <summary>Gets or sets the expiration months.</summary>
        public IList<KeyValuePair<string, int>> ExpirationMonths { get; set; }

        /// <summary>Gets or sets the expiration years.</summary>
        public IList<KeyValuePair<int, int>> ExpirationYears { get; set; }

        /// <summary>Gets or sets the credit card.</summary>
        public CreditCardDto CreditCard { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance can store payment profile.</summary>
        public bool CanStorePaymentProfile { get; set; }

        /// <summary>Gets or sets a value indicating whether to store card info to payment profile if the payment gateway supports it.</summary>
        public bool StorePaymentProfile { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is pay pal.</summary>
        public bool IsPayPal { get; set; }

        /// <summary>Gets or sets the pay pal payer identifier.</summary>
        public string PayPalPayerId { get; set; }

        /// <summary>Gets or sets the pay pal token.</summary>
        public string PayPalToken { get; set; }

        /// <summary>Gets or sets the pay pal payment url.</summary>
        public string PayPalPaymentUrl { get; set; }
    }

    public class CreditCardDto
    {
        /// <summary>Gets or sets the type.</summary>
        public string CardType { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string CardHolderName { get; set; }

        /// <summary>Gets or sets the card number.</summary>
        public string CardNumber { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        public int ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        public int ExpirationYear { get; set; }

        /// <summary>Gets or sets the security code.</summary>
        public string SecurityCode { get; set; }
    }

    public class CostCodeDto
    {
        /// <summary>Gets or sets the cost code.</summary>
        public string CostCode { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }
    }

    public class CustomerOrderTaxDto
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

    public class CartLineCollectionDto
    {
        public IList<CartLine> CartLines { get; set; }
    }

    public class CartCollectionModel : BaseModel
    {
        public IList<Cart> Carts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
