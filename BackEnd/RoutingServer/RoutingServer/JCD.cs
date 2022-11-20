using RoutingServer.JCDClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class JCD
    {
        private HttpClient client;
        public static string apiKey = "apiKey=1de1c64691bd95053065293721d5b0cfd3e5c226";

        private List<Contrat> contrats;

        public async Task InitializeConnexion()
        {
            client = new HttpClient();

            // Récupérer contrats
            string url = "https://api.jcdecaux.com/vls/v3/contracts?" + apiKey;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            contrats = JsonSerializer.Deserialize<List<Contrat>>(responseBody);
        }

        public string GetContracts()
        {
            string response = "";
            foreach (Contrat c in contrats)
            {
                response += c.name + "\n";
            }
            return response;
        }
    }
}
