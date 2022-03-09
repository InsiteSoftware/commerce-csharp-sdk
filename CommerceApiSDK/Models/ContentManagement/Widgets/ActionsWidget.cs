using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CommerceApiSDK.Models.ContentManagement.Converters;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
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
                    int hash = base.GetHashCode();

                    hash = (hash * HashingMultiplier) ^ Type.GetHashCode();
                    hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Icon) ? Icon.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Text) ? Text.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Url) ? RequiresAuth.GetHashCode() : 0);
                    hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, RequiresAuth) ? RequiresAuth.GetHashCode() : 0);
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
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                // Is the same type?
                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((Action)obj);
            }

            public bool Equals(Action obj)
            {
                bool result = base.Equals(obj);

                if (result)
                {
                    result &= Type == obj.Type;
                    result &= Icon == obj.Icon;
                    result &= Text == obj.Text;
                    result &= Url == obj.Url;
                    result &= RequiresAuth == obj.RequiresAuth;
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
                int hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ Layout.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Actions) ? Actions.GetHashCode() : 0);
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
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ActionsWidget)obj);
        }

        public bool Equals(ActionsWidget obj)
        {
            bool result = Equals((Widget)obj);

            if (result)
            {
                result &= Layout == obj.Layout;

                bool areActinsEqual = (Actions == null && obj.Actions == null) ||
                    (Actions != null && obj.Actions != null && Enumerable.SequenceEqual(Actions, obj.Actions));
                result &= areActinsEqual;
            }

            return result;
        }
    }
}