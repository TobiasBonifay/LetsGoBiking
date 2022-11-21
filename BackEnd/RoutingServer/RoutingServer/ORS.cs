using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class ORS
    {
        // Foot walking way
        string baseFootWalkingAddress = "https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf62483fcfcfa7f321433593113c9652931a76";

        private string wayInstructionsResponse = "";

        public async Task GetWayInstructions()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

            HttpResponseMessage response = await client.GetAsync(baseFootWalkingAddress + "&start=8.681495,49.41461&end=8.687872,49.420318");
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
