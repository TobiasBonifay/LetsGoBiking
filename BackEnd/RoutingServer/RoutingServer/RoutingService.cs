using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class RoutingService : IRoutingService
    {
        public string GetStations()
        {
            JCD jcd = new JCD();
            Task task = jcd.InitializeConnexion();
            return jcd.GetContracts();
            
        }

        public string GetWayInstructions()
        {
            ORS ors = new ORS();
            Task task = ors.GetWayInstructions();
            return ors.GetWayInstrictionsResponse();
        }
    }
}
