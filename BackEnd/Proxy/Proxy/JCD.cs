using System.Threading.Tasks;

namespace Proxy
{
    public static class JCD
    {
        private static readonly ProxyCache Cache = new ProxyCache();

        public static async Task<string> InitializeConnexion()
        {
            return await Cache.initializeConnexion();
        }

        public static string GetContractNames()
        {
            return Cache.getContractNames();
        }

        public static async Task<string> GetAllStations()
        {
            return await Cache.getAllStations();
        }

        public static async Task<string> GetStation(string name)
        {
            return await Cache.getStation(name);
        }

        public static async Task<string> GetStationByContract(string contract)
        {
            return await Cache.getStationByContract(contract);
        }

        public static async Task<string> GetCall(string cacheItemName)
        {
            return await Cache.getCall(cacheItemName);
        }
    }
}