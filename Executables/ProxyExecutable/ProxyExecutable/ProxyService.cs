using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyExecutable
{
    public class ProxyService : IProxyService
    {
        private static DateTimeOffset _dtDefault = ObjectCache.InfiniteAbsoluteExpiration;

        public async Task<string> GetAllContracts()
        {
            string result = await JCD.InitializeConnexion();
            return result;
        }

        public string GetAllStations()
        {
            return JCD.GetAllStations().Result;
        }

        public string GetStation(string name)
        {
            return JCD.GetStation(name).Result;
        }

        public string GetStationByContract(string contract)
        {
            return JCD.GetStationByContract(contract).Result;
        }

        public T Get<T>(string cacheItemName)
        {
            return Get<T>(cacheItemName, _dtDefault);
        }

        public T Get<T>(string cacheItemName, double dtSeconds)
        {
            return Get<T>(cacheItemName, DateTimeOffset.Now.AddSeconds(dtSeconds));
        }

        private T Get<T>(string cacheItemName, DateTimeOffset dt)
        {
            ObjectCache cache = MemoryCache.Default;
            var response = (T)cache[cacheItemName];
            if (response != null) return response;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = dt;

            response = JsonSerializer.Deserialize<T>(JCD.GetCall(cacheItemName).Result);
            cache.Set(cacheItemName, response, policy);
            return response;
        }
    }
}
