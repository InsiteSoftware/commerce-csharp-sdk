using System;
using System.Reflection;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Attributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        Inherited = true,
        AllowMultiple = false
    )]
    public sealed class QueryParameterAttribute : Attribute
    {
        public QueryListParameterType? QueryType { get; private set; }

        public QueryOptions? QueryOption { get; private set; }

        public QueryParameterAttribute(QueryOptions queryOption)
        {
            QueryOption = queryOption;
        }

        public QueryParameterAttribute(QueryListParameterType queryType)
        {
            QueryType = queryType;
        }

        public QueryParameterAttribute(QueryListParameterType queryType, QueryOptions queryOption)
        {
            QueryType = queryType;
            QueryOption = queryOption;
        }

        public static QueryListParameterType? GetQueryListParameterType(PropertyInfo properyInfo)
        {
            QueryParameterAttribute customAttribute =
                properyInfo.GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryType;
            }

            return null;
        }

        public static QueryListParameterType? GetQueryListParameterType(object parameter)
        {
            QueryParameterAttribute customAttribute = parameter
                .GetType()
                .GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryType;
            }

            return null;
        }

        public static QueryOptions? GetQueryOption(PropertyInfo properyInfo)
        {
            QueryParameterAttribute customAttribute =
                properyInfo.GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryOption;
            }

            return null;
        }

        public static QueryOptions? GetQueryOption(object parameter)
        {
            QueryParameterAttribute customAttribute = parameter
                .GetType()
                .GetCustomAttribute<QueryParameterAttribute>(true);

            if (customAttribute != null)
            {
                return customAttribute.QueryOption;
            }

            return null;
        }
    }
}
