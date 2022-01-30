namespace CommerceApiSDK.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string StripHtml(this string inputString)
        {
            return Regex.Replace(inputString, @"<[^>]*>", string.Empty);
        }

        public static string GetEnumMemberValue<T>(T value) where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}
