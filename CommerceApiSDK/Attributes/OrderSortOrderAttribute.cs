namespace CommerceApiSDK.Services.Attributes
{
    using System;
    using System.Reflection;

    using CommerceApiSDK.Services.Interfaces;

    public class OrderSortOrderAttribute : Attribute
    {
        public OrderSortOrderAttribute(int groupNumber, string description, string value)
        {
            this.GroupNumber = groupNumber;
            this.Description = description;
            this.Value = value;
        }

        public int GroupNumber { get; private set; }

        public string Description { get; private set; }

        public string Value { get; private set; }

        private static OrderSortOrderAttribute GetSortOrderAttribute(OrderSortOrder orderSortOrder)
        {
            return orderSortOrder
                .GetType()
                .GetRuntimeField(orderSortOrder.ToString())
                .GetCustomAttribute<OrderSortOrderAttribute>();
        }

        public static int GetOrderSortOrderGroupNumber(OrderSortOrder orderSortOrder)
        {
            return GetSortOrderAttribute(orderSortOrder).GroupNumber;
        }

        public static string GetOrderSortOrderDescription(OrderSortOrder orderSortOrder)
        {
            return GetSortOrderAttribute(orderSortOrder).Description;
        }

        public static string GetOrderSortOrderValue(OrderSortOrder orderSortOrder)
        {
            return GetSortOrderAttribute(orderSortOrder).Value;
        }
    }
}
