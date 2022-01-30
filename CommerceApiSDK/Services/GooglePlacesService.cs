﻿namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using CommerceApiSDK.Services.Interfaces;
    using Newtonsoft.Json;
    using Plugin.Geolocator;

    public class GooglePlacesService : ServiceBase, IGooglePlacesService
    {
        private const string GooglePlacesAPIUrl = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json";
        private const string GoogleAPIKey = "AIzaSyDcSlK6wqK4KNhonVLQOPk890sLnfIVXeE";

        public GooglePlacesService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<GooglePlace> GetPlace(string searchQuery)
        {
            try
            {
                searchQuery = WebUtility.UrlEncode(searchQuery);

                var parameters = new List<string>();
                parameters.Add("input=" + searchQuery);
                parameters.Add("inputtype=textquery");
                parameters.Add("fields=formatted_address,geometry");
                parameters.Add("key=" + GoogleAPIKey);

                var locator = CrossGeolocator.Current;

                try
                {
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);

                    if (position != null)
                    {
                        var location = position.Latitude + "," + position.Longitude;
                        parameters.Add("locationbias=circle:100000@" + WebUtility.UrlEncode(location));
                    }
                }
                catch (Exception ex)
                {
                    this.TrackingService.TrackException(ex);
                }

                var url = GooglePlacesAPIUrl + "?" + string.Join("&", parameters);
                var result = await this.GetAsyncStringResultNoCacheNoHost(url).ConfigureAwait(false);

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
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}