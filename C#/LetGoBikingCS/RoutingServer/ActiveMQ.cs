using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace RoutingServer
{
    public class ActiveMQ
    {
        private Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        

        public ActiveMQ()
        {
            // Create a Connection Factory.
            connectionFactory = new ConnectionFactory(connecturi);

            // Create a single Connection from the Connection Factory.

            connection = connectionFactory.CreateConnection();
            connection.Start();
        }

        public void Send(string queueName, string serializedMessage)
        {
            // Create a session from the Connection.
            ISession session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue(queueName);

            // Create a Producer targetting the selected queue.
            IMessageProducer producer = session.CreateProducer(destination);

            // You may configure everything to your needs, for instance:
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

            // Finally, to send messages:
            ITextMessage message = session.CreateTextMessage(serializedMessage);
            producer.Send(message);

            Console.WriteLine("Message sent, check ActiveMQ web interface to confirm.");

            // Don't forget to close your session and connection when finished.
            session.Close();
            connection.Close();
        }
    }
}