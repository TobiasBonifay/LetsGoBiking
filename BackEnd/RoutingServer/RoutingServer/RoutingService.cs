using RoutingServer.ProxyService;

namespace RoutingServer
{
    public class RoutingService : IRoutingService
    {
        private readonly ProxyServiceClient _proxyClient; // allows to communicate with JCDecaux
        private readonly ORS _ors;

        public RoutingService()
        {
            _proxyClient = new ProxyServiceClient();
            _ors = new ORS();
        }

        public string GetStations()
        {
            return _proxyClient.GetAllContracts();
            
        }

        public string GetWayInstructions(string fromCoords, string toCoords)
        {
            _ors.GetWayInstructions(fromCoords, toCoords).Wait();
            return _ors.GetWayInstrictionsResponse();
        }

        public string GetGPSCoordsFromAddress(string address)
        {
            _ors.FindGPSCoords(address).Wait();
            return _ors.GetGPSPositionFound();
        }
    }
}
