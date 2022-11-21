using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ORSClasses
{
    public class segments
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<steps> steps { get; set; }
    }
}
