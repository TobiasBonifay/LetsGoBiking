using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ORSClasses
{
    public class GeoCodeResponse
    {
        public List<GeoCodeFeature> features { get; set; }
        public Properties properties { get; set; }
    }
}
