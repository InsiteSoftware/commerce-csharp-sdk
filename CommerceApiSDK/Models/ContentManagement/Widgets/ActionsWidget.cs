namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using CommerceApiSDK.Models.ContentManagement.Converters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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

    public enum ActionsLayout
    {
        List,
        Grid,
    }

    public class ActionsWidget : Widget
    {
        public class Action
        {
            [JsonConverter(typeof(АctionTypeEnumConverter))]
            public ActionType Type { get; set; }

            public string Icon { get; set; }

            public string Text { get; set; }

            public string Url { get; set; }

            [JsonProperty(PropertyName = "requires_auth")]
            public bool? RequiresAuth { get; set; }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hash = base.GetHashCode();

                    hash = (hash * HashingMultiplier) ^ this.Type.GetHashCode();
                    hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Icon) ? this.Icon.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Text) ? this.Text.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Url) ? this.RequiresAuth.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.RequiresAuth) ? this.RequiresAuth.GetHashCode() : 0);
                    return hash;
                }
            }

            public override bool Equals(object obj)
            {
                // Is null?
                if (obj is null)
                {
                    return false;
                }

                // Is the same object?
                if (object.ReferenceEquals(this, obj))
                {
                    return true;
                }

                // Is the same type?
                if (obj.GetType() != this.GetType())
                {
                    return false;
                }

                return this.Equals((ActionsWidget.Action)obj);
            }

            public bool Equals(ActionsWidget.Action obj)
            {
                var result = base.Equals(obj);

                if (result)
                {
                    result &= this.Type == obj.Type;
                    result &= this.Icon == obj.Icon;
                    result &= this.Text == obj.Text;
                    result &= this.Url == obj.Url;
                    result &= this.RequiresAuth == obj.RequiresAuth;
                }

                return result;
            }
        }

        [JsonConverter(typeof(ActionsLayoutEnumConverter))]
        public ActionsLayout Layout { get; set; }

        [JsonProperty(ItemConverterType = typeof(ActionConverter), PropertyName = "pages")]
        public IList<Action> Actions { get; set; }

        [JsonProperty(ItemConverterType = typeof(ActionConverter), PropertyName = "childWidgets")]
        public new IList<Action> ChildWidgets { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ this.Layout.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Actions) ? this.Actions.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (obj is null)
            {
                return false;
            }

            // Is the same object?
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ActionsWidget)obj);
        }

        public bool Equals(ActionsWidget obj)
        {
            var result = base.Equals((Widget)obj);

            if (result)
            {
                result &= this.Layout == obj.Layout;

                var areActinsEqual = (this.Actions == null && obj.Actions == null) ||
                    (this.Actions != null && obj.Actions != null && Enumerable.SequenceEqual(this.Actions, obj.Actions));
                result &= areActinsEqual;
            }

            return result;
        }
    }
}