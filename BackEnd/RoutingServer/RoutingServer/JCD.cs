using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class JCD
    {
        private HttpClient client;
        private static string apiKey = "";

        public void InitializeConnexion()
        {
            client = new HttpClient();
        }
    }
}
