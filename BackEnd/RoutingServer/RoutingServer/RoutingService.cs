using RoutingServer.JCDClasses;
using RoutingServer.ORSClasses;
using RoutingServer.ProxyService;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class RoutingService : IRoutingService
    {
        private readonly ProxyServiceClient _proxyClient; // allows to communicate with JCDecaux
        private readonly ORS _ors;

        private string proxyContratResp;

        public RoutingService()
        {
            _proxyClient = new ProxyServiceClient();
            proxyContratResp = _proxyClient.GetAllContracts();
            _ors = new ORS();
        }

        public string GetStations()
        {
            return _proxyClient.GetAllStations();
        }

        public string FindClosestStation()
        {
            List<Contract> contrats = JsonSerializer.Deserialize<List<Contract>>(proxyContratResp);
            
            return _ors.GetClosestStation(contrats);
        }

        public string GetWayInstructions(string fromCoords, string toCoords)
        {
            _ors.GetWayInstructions(fromCoords, toCoords).Wait();
            return _ors.GetWayInstrictionsResponse();
        }

        public string GetGPSCoordsFromAddress(string address)
        {
            _ors.FindGPSCoords(address).Wait();
            return _ors.GetGPSPositionFoundCoords();
        }
    }
}
