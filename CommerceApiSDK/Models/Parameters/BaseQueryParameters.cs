namespace CommerceApiSDK.Models.Parameters
{
    public class BaseQueryParameters : BaseQuery
    {
        public virtual int? Page { get; set; }

        /// <summary>Gets or sets the Page Size of rows to return for the Collection.</summary>
        public virtual int? PageSize { get; set; }

        /// <summary>Gets or sets the collection sort.</summary>
        public virtual string Sort { get; set; }
    }
}
