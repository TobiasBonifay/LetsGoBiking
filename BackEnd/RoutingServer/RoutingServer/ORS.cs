using RoutingServer.ORSClasses;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using RoutingServer.JCDClasses;
using System.Threading.Tasks;
using System.Linq;
using RoutingServer.ProxyService;
using static System.Net.WebRequestMethods;

namespace RoutingServer
{
    public class ORS
    {
        private HttpClient httpClient;

        // Foot walking way
        string baseFootWalkingAddress = "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";

        // GPS Position
        private string baseGPSAddress = "https://api.openrouteservice.org/geocode/autocomplete?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76&text=";
        

        private string wayInstructionsResponse;
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


            return stations[0].name;
            // A finir
        }

        public string GetGPSPositionFoundCoords()
        {
            if (gpsPositionFound == null) { return "address not found"; }
            return  gpsPositionFound.features[0].geometry.coordinates[0].ToString().Replace(',', '.') + "," + gpsPositionFound.features[0].geometry.coordinates[1].ToString().Replace(',', '.'); // we take the first address found
        }

        /*
         * Takes coords on entry
         */
        public async Task GetWayInstructions(string from, string to)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

            HttpResponseMessage response = await httpClient.GetAsync(baseFootWalkingAddress + "&start=" + from + "&end=" + to);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            RoadServiceResponse roadServiceResponse = JsonSerializer.Deserialize<RoadServiceResponse>(responseBody);
            Segment segment = roadServiceResponse.features[0].properties.segments[0];
            List<Step> steps = segment.steps;

            wayInstructionsResponse += "Distance: " + segment.distance + "m \n";
            wayInstructionsResponse += "Duration: " + segment.duration + "sec \n";
            foreach (Step s in steps)
            {
                wayInstructionsResponse += s.instruction + "\n";
            }

        }

        public string GetWayInstrictionsResponse()
        {
            return wayInstructionsResponse;
        }

    }
}
