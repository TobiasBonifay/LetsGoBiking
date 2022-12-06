using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ProxyExecutable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/Proxy/ProxyService/");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(ProxyService), httpUrl);

            // Multiple end points can be added to the Service using AddServiceEndpoint() method.
            // Host.Open() will run the service, so that it can be used by any client.

            // Example adding :
            // Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/SimpleCalculator");
            // ServiceHost host = new ServiceHost(typeof(MyCalculatorService.SimpleCalculator), httpUrl, tcpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IProxyService), new WSHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("Proxy server was launched at " + DateTime.Now.ToString());
            Console.WriteLine("Proxy is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}
