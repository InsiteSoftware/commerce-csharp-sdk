using System;

namespace CommerceApiSDK.Models
{
    public class GenericFacet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }
}

