using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingExecutable.ORSClasses
{
    public class GeoCodeResponse
    {
        public List<GeoCodeFeature> features { get; set; }

    }

    public class GeoCodeFeature
    {
        public GeoCodeGeometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class GeoCodeGeometry
    {
        public List<double> coordinates { get; set; } // coordinates[0] == Lon   coordinates[1] == Lat

    }

    public class Properties
    {
        public string locality { get; set; }
    }


}
