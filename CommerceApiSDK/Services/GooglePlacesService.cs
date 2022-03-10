using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CommerceApiSDK.Services.Interfaces;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace CommerceApiSDK.Services
{
    public class GooglePlacesService : ServiceBase, IGooglePlacesService
    {
        public GooglePlacesService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
        }

        public async Task<GooglePlace> GetPlace(string searchQuery)
        {
            try
            {
                searchQuery = WebUtility.UrlEncode(searchQuery);

                List<string> parameters = new List<string>();
                parameters.Add("input=" + searchQuery);
                parameters.Add("inputtype=textquery");
                parameters.Add("fields=formatted_address,geometry");
                parameters.Add("key=" + CommerceAPIConstants.GoogleAPIKey);

                IGeolocator locator = CrossGeolocator.Current;

                try
                {
                    Position position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);

                    if (position != null)
                    {
                        string location = position.Latitude + "," + position.Longitude;
                        parameters.Add("locationbias=circle:100000@" + WebUtility.UrlEncode(location));
                    }
                }
                catch (Exception ex)
                {
                    TrackingService.TrackException(ex);
                }

                string url = CommerceAPIConstants.GooglePlacesAPIUrl + "?" + string.Join("&", parameters);
                string result = await GetAsyncStringResultNoCacheNoHost(url).ConfigureAwait(false);

                if (result != null)
                {
                    dynamic jsonObject = JsonConvert.DeserializeObject(result);

                    if (jsonObject?.candidates != null && jsonObject.candidates.Count > 0)
                    {
                        dynamic candidate = jsonObject.candidates[0];
                        if (candidate.geometry?.location != null)
                        {
                            return new GooglePlace
                            {
                                FormattedName = candidate.formatted_address,
                                Latitude = candidate.geometry.location.lat,
                                Longitude = candidate.geometry.location.lng,
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}