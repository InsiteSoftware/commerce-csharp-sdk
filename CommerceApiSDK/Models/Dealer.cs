namespace CommerceApiSDK.Models
{
    public class Dealer : BaseModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string CountryId { get; set; }

        public string Phone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string WebSiteUrl { get; set; }

        public string HtmlContent { get; set; }

        public double Distance { get; set; }
    }
}
