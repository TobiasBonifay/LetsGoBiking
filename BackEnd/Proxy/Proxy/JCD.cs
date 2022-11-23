using Proxy.JCDClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proxy
{
    public static class JCD
    {

        private static HttpClient client;
        private static string apiKey = "apiKey=1de1c64691bd95053065293721d5b0cfd3e5c226";

        private static List<Contrat> contrats;

        public static async Task InitializeConnexion()
        {
            client = new HttpClient();

            // Récupérer contrats
            string url = "https://api.jcdecaux.com/vls/v3/contracts?" + apiKey;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            contrats = JsonSerializer.Deserialize<List<Contrat>>(responseBody);
        }

        public static string GetContracts()
        {
            StringBuilder response = new StringBuilder();
            foreach (Contrat c in contrats)
            {
                response.Append(c.name).Append("\n");
            }
            return response.ToString();
        }

    }
}
