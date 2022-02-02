using System;
using System.Reflection;

namespace CommerceApiSDK.Services.Attributes
{
    public enum SortOrderOptions
    {
        DoNotDisplay,
    }

    public class SortOrderAttribute : Attribute
    {
        public SortOrderAttribute(string groupTitle, string title, string value)
        {
            Init(groupTitle, title, value, null);
        }

        public SortOrderAttribute(string groupTitle, string title, string value, SortOrderOptions sortOrderOption)
        {
            Init(groupTitle, title, value, sortOrderOption);
        }

        private void Init(string groupTitle, string title, string value, SortOrderOptions? sortOrderOption)
        {
            GroupTitle = groupTitle;
            Title = title;
            Value = value;
            SortOrderOption = sortOrderOption;
        }

        public string GroupTitle { get; private set; }

        public string Title { get; private set; }

        public string Value { get; private set; }

        public SortOrderOptions? SortOrderOption { get; private set; }

        private static SortOrderAttribute GetSortOrderAttribute<T>(T sortOrder) where T : struct
        {
            return sortOrder.GetType().GetRuntimeField(sortOrder.ToString()).GetCustomAttribute<SortOrderAttribute>();
        }

        public static string GetSortOrderGroupTitle<T>(T orderSortOrder) where T : struct
        {
            return GetSortOrderAttribute(orderSortOrder).GroupTitle;
        }

        public static string GetSortOrderTitle<T>(T orderSortOrder) where T : struct
        {
            return GetSortOrderAttribute(orderSortOrder).Title;
        }

        public static string GetSortOrderValue<T>(T orderSortOrder) where T : struct
        {
            return GetSortOrderAttribute(orderSortOrder).Value;
        }

        public static SortOrderOptions? GetSortOrderOption<T>(T orderSortOrder) where T : struct
        {
            return GetSortOrderAttribute(orderSortOrder).SortOrderOption;
        }
    }
}