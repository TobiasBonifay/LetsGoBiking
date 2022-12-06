using Apache.NMS;


namespace RoutingExecutable.ORSClasses
{
    public class ConnectionFactory
    {
        public IConnection CreateConnection()
        {
            return CreateConnection("tcp://localhost:8161");
        }

        public IConnection CreateConnection(string uri)
        {
            var factory = new Apache.NMS.ActiveMQ.ConnectionFactory(uri);
            IConnection connection = factory.CreateConnection("user", "admin");
            return connection;
        }
    }
}
