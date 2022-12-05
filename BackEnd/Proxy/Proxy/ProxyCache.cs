using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Proxy.JCDClasses;

namespace Proxy
{
    internal class ProxyCache : GenericProxyCache<JCDecauxItem>
    {
        private static HttpClient client;
        private static string apiKey = "apiKey=1de1c64691bd95053065293721d5b0cfd3e5c226";

        private static List<Contrat> contrats;
        private static string baseUrlApi = "https://api.jcdecaux.com/vls/v3/";

        private static GenericProxyCache<JCDecauxItem> instance = new GenericProxyCache<JCDecauxItem>();

        public ProxyCache()
        {
        }

        public ProxyCache(DateTimeOffset dtDefault) : base(dtDefault)
        {
        }

        public static void test()
        {
            Task t = JCD.InitializeConnexion();
            t.Wait();

            GenericProxyCache<JCDecauxItem> cache = new GenericProxyCache<JCDecauxItem>();

            string contracts = JCD.GetContractNames();
            foreach (var name in contracts.Split('\n'))
            {
                cache.Add(new CacheItem(name), new CacheItemPolicy());
            }
        }

        public async Task<string>  initializeConnexion()
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

        public string getContractNames()
        {
            var response = new StringBuilder();
            foreach (var c in contrats)
            {
                response.Append(c.name).Append("\n");
            }

            return response.ToString();
        }

        public async Task<string> getAllStations()
        {
            client = new HttpClient();

            // Récupérer stations
            var url = baseUrlApi + "stations" + apiKey;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody.Substring(0, 10);
        }

        public async Task<string> getStation(string name)
        {
            client = new HttpClient();

            // Récupérer les infos d'une station donnée
            var url = baseUrlApi + "stations/" + name + apiKey;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody.Substring(0, 10);
        }

        public async Task<string> getStationByContract(string contract)
        {
            client = new HttpClient();

            // Récupérer stations
            var url = baseUrlApi + "stations/?contract=" + contract + "&" + apiKey;
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<string> getCall(string cacheItemName)
        {
            Console.WriteLine("GetCall");
            Console.WriteLine(cacheItemName);
            
            if (instance.Contains(cacheItemName))
            {
                Console.WriteLine("Cache hit");
                return instance.Get(cacheItemName).ToString();
            }
            else
            {
                Console.WriteLine("Cache miss");
                var response = await getStationByContract(cacheItemName);
                instance.Add(new CacheItem(cacheItemName), new CacheItemPolicy());
                return response;
            }
        }
    }
}