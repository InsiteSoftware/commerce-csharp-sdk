using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class Language : BaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the language code.</summary>
        public string LanguageCode { get; set; }

        /// <summary>Gets or sets the culture code.</summary>
        public string CultureCode { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the image file path.</summary>
        public string ImageFilePath { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is default.</summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is enabled on this site.</summary>
        public bool IsLive { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}
