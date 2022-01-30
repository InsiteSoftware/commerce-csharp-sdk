namespace CommerceApiSDK.Attributes
{
    using System;
    using System.Reflection;

    public enum QueryListParameterType
    {
        RepeatingParameter = 0,
        CommaSeparated = 1,
    }

    public enum QueryOptions
    {
        DoNotEncode,
        DoNotQuery,
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class QueryParameterAttribute : Attribute
    {
        public QueryListParameterType? QueryType { get; private set; }

        public QueryOptions? QueryOption { get; private set; }

        public QueryParameterAttribute(QueryOptions queryOption)
        {
            this.QueryOption = queryOption;
        }

        public QueryParameterAttribute(QueryListParameterType queryType)
        {
            this.QueryType = queryType;
        }

        public QueryParameterAttribute(QueryListParameterType queryType, QueryOptions queryOption)
        {
            this.QueryType = queryType;
            this.QueryOption = queryOption;
        }

        public static QueryListParameterType? GetQueryListParameterType(PropertyInfo properyInfo)
        {
            var customAttribute = properyInfo.GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryType;
            }

            return null;
        }

        public static QueryListParameterType? GetQueryListParameterType(object parameter)
        {
            var customAttribute = parameter.GetType().GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryType;
            }

            return null;
        }

        public static QueryOptions? GetQueryOption(PropertyInfo properyInfo)
        {
            var customAttribute = properyInfo.GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryOption;
            }

            return null;
        }

        public static QueryOptions? GetQueryOption(object parameter)
        {
            var customAttribute = parameter.GetType().GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryOption;
            }

            return null;
        }
    }
}