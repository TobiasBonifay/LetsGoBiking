using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ORSClasses
{
    public class GeoCodeFeature
    {
        public GeoCodeGeometry geometry { get; set; }
        public Properties properties { get; set; }
    }
}
