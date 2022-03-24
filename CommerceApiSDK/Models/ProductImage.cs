using System;

namespace CommerceApiSDK.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public int SortOrder { get; set; }

        public string Name { get; set; }

        public string SmallImagePath { get; set; }

        public string MediumImagePath { get; set; }

        public string LargeImagePath { get; set; }

        public string AltText { get; set; }

        public string ImageType { get; set; }
    }
}

