using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class CountryCollection : BaseModel
    {
        public IList<Country> Countries { get; set; }
    }
}

