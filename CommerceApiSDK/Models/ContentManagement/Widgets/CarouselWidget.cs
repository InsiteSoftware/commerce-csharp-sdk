namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System;
    using System.Linq;
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
                hash = (hash * HashingMultiplier) ^ this.TimerSpeed.GetHashCode();
                hash = (hash * HashingMultiplier) ^ this.AnimationSpeed.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (object.ReferenceEquals(null, obj))
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

            return this.Equals((CarouselWidget)obj);
        }

        public bool Equals(CarouselWidget widget)
        {
            var result = base.Equals((Widget)widget);

            if (result)
            {
                result &= this.TimerSpeed == widget.TimerSpeed
                    && this.AnimationSpeed == widget.AnimationSpeed
                    && this.ChildWidgets.Count.Equals(widget.ChildWidgets.Count);
            }

            // Loop through all child widgets
            if (result)
            {
                for (int i = 0; i < this.ChildWidgets.Count; i++)
                {
                    if (!this.ChildWidgets[i].Equals(widget.ChildWidgets[i]))
                    {
                        return false;
                    }
                }
            }

            return result;
        }
    }
}
