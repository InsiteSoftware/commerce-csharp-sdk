using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class CategoryResult : BaseModel
    {
        public IList<Category> Categories { get; set; }
    }
}
