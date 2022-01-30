namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class CategoryResult : BaseModel
    {
        public IList<Category> Categories { get; set; }
    }
}
