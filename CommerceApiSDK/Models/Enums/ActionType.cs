using System;
using System.Runtime.Serialization;

namespace CommerceApiSDK.Models.Enums
{
    public enum ActionType
    {
        Unknown,

        // Custom CMS options
        Custom,

        // Predefined options
        Categories,
        [EnumMember(Value = "Brand")]
        Brands,
        Search,
        QuickOrder,
        OrderHistory,
        Lists,
        SavedOrders,
        ChangeCustomer,
        ViewAccountOnWebsite,
        Settings,
        SignOut,
        [EnumMember(Value = "Locations")]
        LocationFinder,

        // Developer Options
        ForceCrash,
        ToggleLogging,
        Invoices,
        SavedPayments,
        Quotes,
        [EnumMember(Value = "VendorManagedInventory")]
        VMI,

        // VMI Actions
        CountInventory,
        CreateOrder,
        ChangeLocation,
    }
}
