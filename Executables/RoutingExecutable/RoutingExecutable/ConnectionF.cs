using Apache.NMS;
using Apache.NMS.ActiveMQ;


namespace RoutingExecutable.ORSClasses
{
    public class ConnectionF
    {
        public IConnection CreateConnection()
        {
            return CreateConnection("activemq:tcp://localhost:61616");
        }

        public IConnection CreateConnection(string uri)
        {
            var factory = new Apache.NMS.ActiveMQ.ConnectionFactory(uri);
            IConnection connection = factory.CreateConnection("user", "admin");
            connection.Start();
            return connection;
        }
    }
}
