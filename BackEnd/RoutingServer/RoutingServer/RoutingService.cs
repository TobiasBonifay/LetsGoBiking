using RoutingServer.ProxyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class RoutingService : IRoutingService
    {
        private ProxyServiceClient proxyClient; // allows to communicate with JCDecaux
        private ORS ors;

        public RoutingService()
        {
            proxyClient = new ProxyServiceClient();
            ors = new ORS();
        }

        public string GetStations()
        {
            return proxyClient.GetAllContracts();
            
        }

        public string GetWayInstructions()
        {
            ors.GetWayInstructions().Wait();
            return ors.GetWayInstrictionsResponse();
        }

        public string GetGPSCoordsFromAddress(string address)
        {
            ors.FindGPSCoords(address);
            return ors.GetGPSPositionFound();
        }
    }
}
