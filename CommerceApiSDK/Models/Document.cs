using System;

namespace CommerceApiSDK.Models
{
    public class Document
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string FilePath { get; set; }

        [Obsolete("Use FilePath instead")]
        public string FileUrl { get; set; }

        public string DocumentType { get; set; }

        public Guid? LanguageId { get; set; }

        [Obsolete("Use DocumentType instead")]
        public string FileTypeString { get; set; }
    }
}
