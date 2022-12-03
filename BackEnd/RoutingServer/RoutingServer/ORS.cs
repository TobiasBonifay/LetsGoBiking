﻿using RoutingServer.ORSClasses;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using RoutingServer.JCDClasses;
using System.Threading.Tasks;
using System.Linq;
using RoutingServer.ProxyService;
using static System.Net.WebRequestMethods;
using System.Device.Location;

namespace RoutingServer
{
    public class ORS
    {
        private HttpClient httpClient;

        // Foot walking way
        string baseFootWalkingAddress = "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";
        string baseByBikeAddress = "https://api.openrouteservice.org/v2/directions/cycling-regular?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";
        // GPS Position
        private string baseGPSAddress = "https://api.openrouteservice.org/geocode/autocomplete?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76&text=";
        

        private GeoCodeResponse gpsPositionFound;

        public ORS()
        {
            httpClient = new HttpClient();
        }

        public async Task FindGPSCoords(string address)
        {
            httpClient.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = await httpClient.GetAsync(baseGPSAddress + address );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            gpsPositionFound = JsonSerializer.Deserialize<GeoCodeResponse>(responseBody);
            
        }

        public string GetClosestStationAsync(List<Contract> contracts, string address)
        {
            FindGPSCoords(address).Wait();
            string actualCity = gpsPositionFound.features[0].properties.locality; // test of start position only
            List<string> citiesToCheck = new List<string>();
            Contract closestContract = null;

            foreach (Contract c in contracts)
            {
                citiesToCheck.Clear();
                if (c.cities != null)
                    citiesToCheck.AddRange(c.cities);
                if (citiesToCheck.Contains(actualCity))
                {
                    closestContract = c;
                    break;
                }
            }

            if (closestContract == null) { return "Service pas disponible dans votre ville, ville de " + actualCity ; }

            ProxyServiceClient proxy = new ProxyServiceClient();
            string stationsJson = proxy.GetStationByContract(closestContract.name);
            List<Station> stations = JsonSerializer.Deserialize<List<Station>>(stationsJson);

            Station closestStation = stations[0];
            GeoCoordinate s1 = new GeoCoordinate(closestStation.position.latitude, closestStation.position.longitude);
            double distMin = s1.GetDistanceTo(new GeoCoordinate(gpsPositionFound.features[0].geometry.coordinates[1], gpsPositionFound.features[0].geometry.coordinates[0]));
            
            foreach (Station s in stations)
            {
                GeoCoordinate s2 = new GeoCoordinate(s.position.latitude, s.position.longitude);
                if ( (s1.GetDistanceTo(s2) < distMin) && s.totalStands.availabilities.bikes > 0) 
                {
                    closestStation = s;
                    s1.Latitude = s2.Latitude;
                    s1.Longitude = s2.Longitude;
                }
            }

            return closestStation.position.longitude.ToString().Replace(',', '.') + "," + closestStation.position.latitude.ToString().Replace(',', '.');
            // A finir
        }

        public string GetGPSPositionFoundCoords()
        {
            if (gpsPositionFound == null) { return "address not found"; }
            return  gpsPositionFound.features[0].geometry.coordinates[0].ToString().Replace(',', '.') + "," + gpsPositionFound.features[0].geometry.coordinates[1].ToString().Replace(',', '.'); // we take the first address found
        }

        /*
         * Takes coords on entry and boolean to differentiate foot walking instructions from by bike instrictions
         */
        public async Task<Segment> GetWayInstructions(string from, string to, bool usingBike)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

            string httpAddress = baseFootWalkingAddress;
            if (usingBike) httpAddress = baseByBikeAddress;

            HttpResponseMessage response = await httpClient.GetAsync(httpAddress + "&start=" + from + "&end=" + to);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            RoadServiceResponse roadServiceResponse = JsonSerializer.Deserialize<RoadServiceResponse>(responseBody);
            Segment segment = roadServiceResponse.features[0].properties.segments[0];

            return segment;
        }

        public string GetWayInstrictionsResponse(Segment segment)
        {
            string wayInstructionsResponse = null;
            List<Step> steps = segment.steps;
            wayInstructionsResponse += "Distance: " + segment.distance + "m \n";
            wayInstructionsResponse += "Duration: " + segment.duration + "sec \n";
            foreach (Step s in steps)
            {
                wayInstructionsResponse += s.instruction + "\n";
            }
            return wayInstructionsResponse;
        }

    }
}
