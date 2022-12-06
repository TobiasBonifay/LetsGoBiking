using RoutingExecutable.ORSClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using ConnectionFactory = RoutingExecutable.ORSClasses.ConnectionFactory;

namespace RoutingExecutable
{
    public class Producer
    {
        private const string QueueName = "ITINERARY";

        private IConnection _connection;

        private IMessageProducer _producer;

        private ISession _session;

        private ConnectionFactory factory = new ConnectionFactory();

        public Producer()
        {
            // Create a single Connection from the Connection Factory.
            _connection = factory.CreateConnection();
            _connection.Start();

            // Create a session from the Connection.
            _session = _connection.CreateSession();

            // Use the session to target a queue.
            var destination = _session.GetQueue(QueueName);
            // new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(QueueName);

            // Create a Producer targeting the selected queue.
            _producer = _session.CreateProducer(destination);

            init();
        }

        public void init()
        {
            // You may configure everything to your needs, for instance:
            _producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
            _producer.RequestTimeout = new TimeSpan(0, 0, 30);
            _producer.TimeToLive = new TimeSpan(0, 0, 30);
        }

        public void SendMessage(string sInstruction)
        {
            _producer.Send(_session.CreateTextMessage(sInstruction));
            Console.WriteLine(sInstruction);
        }

        public void Close()
        {
            // Don't forget to close your session and connection when finished.
            _session.Close();
            _connection.Close();
        }
    }
}
