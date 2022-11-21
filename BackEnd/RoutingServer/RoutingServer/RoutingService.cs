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
        public string GetStations()
        {
            JCD jcd = new JCD();
            jcd.InitializeConnexion().Wait();
            return jcd.GetContracts();
            
        }

        public string GetWayInstructions()
        {
            ORS ors = new ORS();
            ors.GetWayInstructions().Wait();
            return ors.GetWayInstrictionsResponse();
        }
    }
}
