using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class StateCollection : BaseModel
    {
        /// <summary>Gets or sets the states.</summary>
        public IList<State> States { get; set; }
    }
}

