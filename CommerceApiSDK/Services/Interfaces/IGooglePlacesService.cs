namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;

    public class GooglePlace
    {
        public string FormattedName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public interface IGooglePlacesService
    {
        Task<GooglePlace> GetPlace(string searchQuery);
    }
}
