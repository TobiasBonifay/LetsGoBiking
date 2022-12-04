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
        private static string baseUrlApi = "https://api.jcdecaux.com/vls/v3/";

        public static async Task<string> InitializeConnexion()
        {
            client = new HttpClient();

            // Récupérer contrats
            var url = baseUrlApi + "contracts" + "?" + apiKey;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            contrats = JsonSerializer.Deserialize<List<Contrat>>(responseBody);

            return responseBody;
        }

        public static string GetContractNames()
        {
            var response = new StringBuilder();
            foreach (var c in contrats)
            {
                response.Append(c.name).Append("\n");
            }

            return response.ToString();
        }

        public static async Task<string> GetAllStations()
        {
            client = new HttpClient();

            // Récupérer stations
            var url = baseUrlApi + "stations" + apiKey;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody.Substring(0, 10);
        }

        public static async Task<string> GetStation(string name)
        {
            client = new HttpClient();

            // Récupérer les infos d'une station donnée
            var url = baseUrlApi + "stations/" + name + apiKey;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody.Substring(0, 10);
        }

        public static async Task<string> GetStationByContract(string contract)
        {
            client = new HttpClient();

            // Récupérer stations
            var url = baseUrlApi + "stations/?contract=" + contract + "&" + apiKey;
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public static async Task<string> GetCall(string cacheItemName)
        {
            // TODO call cache ?
            Console.WriteLine("GetCall");
            Console.WriteLine(cacheItemName);
            return await GetStationByContract(cacheItemName);
        }
    }
}