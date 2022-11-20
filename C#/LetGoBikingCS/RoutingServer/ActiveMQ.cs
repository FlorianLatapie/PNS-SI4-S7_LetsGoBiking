using System;
using System.Collections.Generic;
using System.Linq;
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

        /*
        public void Send(string queueName, string serializedMessage)
        {
            var session = PrepareSend(queueName, out var producer);

            // Finally, to send messages:
            var message = session.CreateTextMessage(serializedMessage);
            producer.Send(message);

            FinalizeSend(session);
        }*/

        public void Send(string queueName, List<string> serializedMessages)
        {
            var session = PrepareSend(queueName, out var producer);

            // Finally, to send messages:
            foreach (var message in serializedMessages.Select(serializedMessage => session.CreateTextMessage(serializedMessage)))
                producer.Send(message);

            FinalizeSend(session);
        }

        private void FinalizeSend(ISession session)
        {
            // Don't forget to close your session and connection when finished.
            session.Close();
            connection.Close();
        }

        private ISession PrepareSend(string queueName, out IMessageProducer producer)
        {
            // Create a session from the Connection.
            var session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue(queueName);

            // Create a Producer targetting the selected queue.
            producer = session.CreateProducer(destination);

            // You may configure everything to your needs, for instance:
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
            return session;
        }
    }
}