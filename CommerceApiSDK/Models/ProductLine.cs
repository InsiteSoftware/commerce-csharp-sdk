using System;

namespace CommerceApiSDK.Models
{
    public class ProductLine
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }
    }
}
