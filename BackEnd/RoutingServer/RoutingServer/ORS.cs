using RoutingServer.ORSClasses;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class ORS
    {
        private HttpClient httpClient;

        // Foot walking way
        string baseFootWalkingAddress = "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";

        // GPS Position
        private string baseGPSAddress = "https://api.openrouteservice.org/geocode/autocomplete?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76&text=";
        //private string baseGPSParams = "&format=json&polygon=1&addressdetails=1";

        private string wayInstructionsResponse = "";
        private GeoCodeResponse gpsPositionFound;

        public ORS()
        {
            httpClient = new HttpClient();
        }

        public async void FindGPSCoords(string address)
        {
            httpClient.DefaultRequestHeaders.Clear();
            HttpResponseMessage response = await httpClient.GetAsync(baseGPSAddress + address);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            GeoCodeResponse gpsPositions = JsonSerializer.Deserialize<GeoCodeResponse>(responseBody);
            gpsPositionFound = gpsPositions; // we take the first address found
        }

        public string GetGPSPositionFound() { 
            if (gpsPositionFound == null) { return "address not found"; }
            return gpsPositionFound.ToString();
        }

        public async Task GetWayInstructions()
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

            HttpResponseMessage response = await httpClient.GetAsync(baseFootWalkingAddress + "&start=8.681495,49.41461&end=8.687872,49.420318");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            wayInstructionsResponse = responseBody;
        }

        public string GetWayInstrictionsResponse()
        {
            return wayInstructionsResponse;
        }

    }
}
