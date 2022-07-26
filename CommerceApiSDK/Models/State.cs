using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class State : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the abbreviation.</summary>
        public string Abbreviation { get; set; }

        /// <summary>Gets or sets the states.</summary>
        public IList<State> States { get; set; }
    }
}

