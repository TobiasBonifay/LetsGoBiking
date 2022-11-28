using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.JCDClasses
{
    public class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public Stand totalStands { get; set; }
    }

    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
