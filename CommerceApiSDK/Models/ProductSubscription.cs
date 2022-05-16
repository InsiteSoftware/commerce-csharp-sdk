using System;

namespace CommerceApiSDK.Models
{
    public class ProductSubscription
    {
        public bool SubscriptionAddToInitialOrder { get; set; }

        public bool SubscriptionAllMonths { get; set; }

        public bool SubscriptionApril { get; set; }

        public bool SubscriptionAugust { get; set; }

        public string SubscriptionCyclePeriod { get; set; }

        public bool SubscriptionDecember { get; set; }

        public bool SubscriptionFebruary { get; set; }

        public bool SubscriptionFixedPrice { get; set; }

        public bool SubscriptionJanuary { get; set; }

        public bool SubscriptionJuly { get; set; }

        public bool SubscriptionJune { get; set; }

        public bool SubscriptionMarch { get; set; }

        public bool SubscriptionMay { get; set; }

        public bool SubscriptionNovember { get; set; }

        public bool SubscriptionOctober { get; set; }

        public int SubscriptionPeriodsPerCycle { get; set; }

        public bool SubscriptionSeptember { get; set; }

        public Guid? SubscriptionShipViaId { get; set; }

        public int SubscriptionTotalCycles { get; set; }
    }
}
