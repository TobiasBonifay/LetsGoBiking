using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Device.Location;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Text.Json;
using RoutingExecutable.ORSClasses;
using RoutingExecutable.JCDClasses;
using RoutingExecutable.ProxyService;

namespace RoutingExecutable.ORSClasses
{
    public class ORS
    {
        string baseByBikeAddress =
            "https://api.openrouteservice.org/v2/directions/cycling-regular?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";

        // Foot walking way
        string baseFootWalkingAddress =
            "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";

        // GPS Position
        private string baseGPSAddress =
            "https://api.openrouteservice.org/geocode/search?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76&text=";


        private GeoCodeResponse gpsPositionFound;
        private HttpClient httpClient;
        private Producer producer;

        public ORS()
        {
            httpClient = new HttpClient();
        }

        public async Task FindGPSCoords(string address)
        {
            httpClient.DefaultRequestHeaders.Clear();
            var response = await httpClient.GetAsync(baseGPSAddress + address);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            gpsPositionFound = JsonSerializer.Deserialize<GeoCodeResponse>(responseBody);
        }

        public string GetClosestStationAsync(List<Contract> contracts, string address)
        {
            FindGPSCoords(address).Wait();
            if (gpsPositionFound == null || gpsPositionFound.features.Count < 1) return "404"; // no result found

            var actualCity = gpsPositionFound.features[0].properties.locality;
            if (actualCity == null) return "404"; // Actual city not found

            var citiesToCheck = new List<string>();
            Contract closestContract = null;
            foreach (var c in contracts)
            {
                citiesToCheck.Clear();
                if (c.cities != null)
                    citiesToCheck.AddRange(c.cities);
                if (citiesToCheck.Contains(actualCity) || actualCity.Equals("Besancon"))
                {
                    closestContract = c;
                    break;
                }
            }

            if (closestContract == null) return "404"; // Fatal error: no locality found

            var stations = JsonSerializer.Deserialize<List<Station>>(new ProxyServiceClient().GetStationByContract(closestContract.name));
            if (stations == null) return "404"; // Fatal error: no station found

            var closestStation = stations[0];
            var s1 = new GeoCoordinate(closestStation.position.latitude, closestStation.position.longitude);
            var distMin = s1.GetDistanceTo(new GeoCoordinate(gpsPositionFound.features[0].geometry.coordinates[1],
                gpsPositionFound.features[0].geometry.coordinates[0]));

            foreach (var s in stations)
            {
                var s2 = new GeoCoordinate(s.position.latitude, s.position.longitude);
                if ((s1.GetDistanceTo(s2) < distMin) && s.totalStands.availabilities.bikes > 0)
                {
                    closestStation = s;
                    s1.Latitude = s2.Latitude;
                    s1.Longitude = s2.Longitude;
                }
            }

            return closestStation.position.longitude.ToString().Replace(',', '.') + "," +
                   closestStation.position.latitude.ToString().Replace(',', '.');
        }

        /**
         * @return the first match
         */
        public string GetGPSPositionFoundCoords()
        {
            if (gpsPositionFound == null)
            {
                return "Error : Address not found";
            }

            if (gpsPositionFound.features.Count == 0)
            {
                return "Error : Unknown Address ";
            }

            return gpsPositionFound.features[0].geometry.coordinates[0].ToString().Replace(',', '.') + "," +
                   gpsPositionFound.features[0].geometry.coordinates[1].ToString()
                       .Replace(',', '.'); // we take the first address found
        }

        /*
         * Get the route between two points
         * @param from, to : string of coordinates
         * @param byBike : boolean to differentiate foot walking instructions from by bike instructions
         * @return : Task<Segment> containing instructions with distance and duration
         */
        public async Task<Segment> GetWayInstructions(string from, string to, bool usingBike)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept",
                "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

            var httpAddress = baseFootWalkingAddress;
            if (usingBike) httpAddress = baseByBikeAddress;

            var response = await httpClient.GetAsync(httpAddress + "&start=" + from + "&end=" + to);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            RoadServiceResponse roadServiceResponse = JsonSerializer.Deserialize<RoadServiceResponse>(responseBody);
            if (roadServiceResponse != null) return roadServiceResponse.features[0].properties.segments[0];

            Console.Write("[Serializer]Error in road service response");
            return null;
        }

        /**
         * Get steps of the way
         * @param segment Segment Object (double distance, double duration, List<Step> steps)
         * @return string with the instructions, distance and duration
         */
        public string GetWayInstructionsResponse(Segment segment)
        {
            var wayInstructionsResponse = new StringBuilder();
            var distance = segment.distance;
            var duration = segment.duration;

            wayInstructionsResponse.Append("Segment distance : ");
            if (distance > 1000)
                wayInstructionsResponse.Append((distance / 1000).ToString("0.00")).Append(" km");
            else
                wayInstructionsResponse.Append(distance.ToString("0.0")).Append(" m");

            wayInstructionsResponse.Append("\n").Append("Segment duration : ");

            if (duration > 3600)
            {
                wayInstructionsResponse.Append(Math.DivRem((int)duration, 3600, out int remainder)).Append(" h ");
                duration = remainder;
            }

            if (duration > 60)
            {
                wayInstructionsResponse.Append(Math.DivRem((int)duration, 60, out int remainder)).Append(" min ");
                duration = remainder;
            }

            if (duration > 0)
                wayInstructionsResponse.Append(duration.ToString("0")).Append(" s");

            wayInstructionsResponse.Append("\n").Append("Instructions : \n");

            //producer.SendMessage(wayInstructionsResponse.ToString());

            var steps = segment.steps;
            var i = 0;
            foreach (var s in steps)
            {
                wayInstructionsResponse.Append("Step ").Append(i++).Append(" -> ").Append(s.instruction).Append("\n");
                //producer.SendMessage(s.instruction);
            }
            return wayInstructionsResponse.ToString();
        }

        public void addMessageToQueue(string message)
        {
            producer.SendMessage(message);
        }

        public void initMessage()
        {
            producer = new Producer();
            producer.init();
        }

        public void endOfMessage()
        {
            producer.SendMessage("end");
            producer.Close();
        }
    }
}
