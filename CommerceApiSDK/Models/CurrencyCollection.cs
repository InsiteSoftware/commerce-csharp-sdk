using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class CurrencyCollection : BaseModel
    {
        public IList<Currency> Currencies { get; set; }
    }
}
