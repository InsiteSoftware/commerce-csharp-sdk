namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class CarouselWidget : Widget
    {
        [JsonProperty(ItemConverterType = typeof(WidgetConverter), PropertyName = "childWidgets")]
        public new IList<CarouselSlideWidget> ChildWidgets { get; set; }

        public int TimerSpeed { get; set; }

        public int AnimationSpeed { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode();
                hash = (hash * HashingMultiplier) ^ TimerSpeed.GetHashCode();
                hash = (hash * HashingMultiplier) ^ AnimationSpeed.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (ReferenceEquals(null, obj))
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

            return Equals((CarouselWidget)obj);
        }

        public bool Equals(CarouselWidget widget)
        {
            bool result = Equals((Widget)widget);

            if (result)
            {
                result &= TimerSpeed == widget.TimerSpeed
                    && AnimationSpeed == widget.AnimationSpeed
                    && ChildWidgets.Count.Equals(widget.ChildWidgets.Count);
            }

            // Loop through all child widgets
            if (result)
            {
                for (int i = 0; i < ChildWidgets.Count; i++)
                {
                    if (!ChildWidgets[i].Equals(widget.ChildWidgets[i]))
                    {
                        return false;
                    }
                }
            }

            return result;
        }
    }
}
