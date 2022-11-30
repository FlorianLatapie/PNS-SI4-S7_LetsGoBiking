using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace RoutingServer
{
    public class ActiveMQ
    {
        public static void run()
        {
            // Create a Connection Factory.
            Uri connecturi = new Uri("activemq:tcp://localhost:61616");
            ConnectionFactory connectionFactory = new ConnectionFactory(connecturi);

            // Create a single Connection from the Connection Factory.
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();

            // Create a session from the Connection.
            Apache.NMS.ISession session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue("testeeeeeeeeee");

            // Create a Producer targetting the selected queue.
            IMessageProducer producer = session.CreateProducer(destination);

            // You may configure everything to your needs, for instance:
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

            // Finally, to send messages:
            ITextMessage message = session.CreateTextMessage("Hello World 2");
            producer.Send(message);

            Console.WriteLine("Message sent, check ActiveMQ web interface to confirm.");
            Console.ReadLine();

            // Don't forget to close your session and connection when finished.
            session.Close();
            connection.Close();
        }
    }
}