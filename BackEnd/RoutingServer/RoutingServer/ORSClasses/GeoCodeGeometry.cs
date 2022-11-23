using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ORSClasses
{
    public class GeoCodeGeometry
    {
        public double[] coordinates { get; set; }

        public double getLon() { return coordinates[0]; }
        public double getLat() { return coordinates[1];}
    }
}
