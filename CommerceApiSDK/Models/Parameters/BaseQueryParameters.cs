namespace CommerceApiSDK.Models.Parameters
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using CommerceApiSDK.Attributes;

    public class BaseQueryParameters
    {
        public virtual int? Page { get; set; }

        /// <summary>Gets or sets the Page Size of rows to return for the Collection.</summary>
        public virtual int? PageSize { get; set; }

        /// <summary>Gets or sets the collection sort.</summary>
        public virtual string Sort { get; set; }

        public virtual string ToQueryString()
        {
            var properties = this.GetType().GetProperties().Where(p => p.GetValue(this, null) != null && QueryParameterAttribute.GetQueryOption(p) != QueryOptions.DoNotQuery).ToList();

            List<string> query = new List<string>();

            foreach (var p in properties)
            {
                if (p.GetValue(this) is IList list)
                {
                    if (list.Count > 0)
                    {
                        var queryListParameterType = QueryParameterAttribute.GetQueryListParameterType(p);

                        if (queryListParameterType.HasValue && queryListParameterType.Value == QueryListParameterType.CommaSeparated)
                        {
                            var listItems = new List<string>();

                            foreach (var item in list)
                            {
                                listItems.Add(HttpUtility.UrlEncode(item.ToString()));
                            }

                            query.Add(p.Name + "=" + string.Join(",", listItems));
                        }
                        else
                        {
                            foreach (var item in list)
                            {
                                query.Add(p.Name + "=" + HttpUtility.UrlEncode(item.ToString()));
                            }
                        }
                    }
                }
                else
                {
                    var value = p.GetValue(this, null).ToString();

                    var queryOption = QueryParameterAttribute.GetQueryOption(p);
                    switch (queryOption)
                    {
                        case QueryOptions.DoNotEncode:
                            query.Add(p.Name + "=" + value);
                            break;
                        default:
                            query.Add(p.Name + "=" + HttpUtility.UrlEncode(value));
                            break;
                    }
                }
            }

            return string.Concat("?", string.Join("&", query.ToArray()));
        }
    }
}
