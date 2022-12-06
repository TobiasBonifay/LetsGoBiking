using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace RoutingServer
{
    public class ConnectionFactory
    {
        public IConnection CreateConnection()
        {
           return CreateConnection("tcp://localhost:8161");
        }

        public IConnection CreateConnection(string uri)
        {
            var factory = new Apache.NMS.ActiveMQ.ConnectionFactory();
            IConnection connection = factory.CreateConnection("user", "admin");
            return connection;
        }
    }
}