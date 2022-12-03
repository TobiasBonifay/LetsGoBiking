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

        // Returns GPS Coords of the closest station to given address
        public string FindClosestStation(string address)
        {
            List<Contract> contrats = JsonSerializer.Deserialize<List<Contract>>(proxyContratResp);
            
            return _ors.GetClosestStationAsync(contrats, address);
        }

        // Returns foot-walking instructions 
        public string GetWayInstructions(string fromCoords, string startClosesetStztion, string toCoords, string endClosestStation)
        {
            Segment startToStation = _ors.GetWayInstructions(fromCoords, startClosesetStztion).Result;
            Segment endClosestStationToEnd = _ors.GetWayInstructions(endClosestStation, toCoords).Result;
            Segment fromTo = _ors.GetWayInstructions(fromCoords, toCoords).Result;

            if ( (startToStation.duration + endClosestStationToEnd.duration) > fromTo.duration) 
            {
                return _ors.GetWayInstrictionsResponse(fromTo);
            }

            return _ors.GetWayInstrictionsResponse(startToStation) + _ors.GetWayInstrictionsResponse(endClosestStationToEnd);
        }

        // Returns GPS Coords of given address 
        public string GetGPSCoordsFromAddress(string address)
        {
            _ors.FindGPSCoords(address).Wait();
            return _ors.GetGPSPositionFoundCoords();
        }
    }
}
