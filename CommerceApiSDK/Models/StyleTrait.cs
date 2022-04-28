using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class StyleTrait
    {
        public Guid StyleTraitId { get; set; }

        public string Name { get; set; }

        public string NameDisplay { get; set; }

        public string UnselectedValue { get; set; }

        public int SortOrder { get; set; }

        public IList<StyleValue> StyleValues { get; set; }

        // for V2
        public Guid Id { get; set; }

        public IList<StyleValue> TraitValues { get; set; }
    }
}

