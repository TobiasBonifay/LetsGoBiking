using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RoutingExecutable.ORSClasses;

namespace RoutingExecutable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/RoutingServer/RoutingService/");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(RoutingService), httpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IRoutingService), new BasicHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("RoutingService started at " + DateTime.Now.ToString());
            Console.WriteLine("RoutingService is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}
