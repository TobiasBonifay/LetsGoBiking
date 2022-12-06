using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingExecutable.ORSClasses
{
    public class RoadServiceResponse
    {
        public List<Feature> features { get; set; }
    }

    public class Feature
    {
        public Property properties { get; set; }
    }

    public class Property
    {
        public List<Segment> segments { get; set; }
    }

    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public string instruction { get; set; }
    }
}
