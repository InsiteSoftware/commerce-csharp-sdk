using System;
using System.ComponentModel;

namespace CommerceApiSDK.Models.Enums
{
    public enum FulfillmentMethodType : int
    {
        [Description("Ship")]
        [FulfillmentMethodDisplayName("Ship")]
        Ship,

        [Description("PickUp")]
        [FulfillmentMethodDisplayName("Pick Up")]
        PickUp,
    }
}
