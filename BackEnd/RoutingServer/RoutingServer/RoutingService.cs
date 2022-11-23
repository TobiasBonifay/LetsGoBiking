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

        public string GetWayInstructions()
        {
            _ors.GetWayInstructions().Wait();
            return _ors.GetWayInstrictionsResponse();
        }

        public string GetGPSCoordsFromAddress(string address)
        {
            _ors.FindGPSCoords(address);
            return _ors.GetGPSPositionFound();
        }
    }
}
