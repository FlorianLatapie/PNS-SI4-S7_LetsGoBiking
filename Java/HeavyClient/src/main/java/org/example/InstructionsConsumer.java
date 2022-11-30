package org.example;

import org.apache.activemq.ActiveMQConnectionFactory;

import javax.jms.*;
import java.beans.ExceptionListener;

public class InstructionsConsumer implements ExceptionListener {
    public void run(String queueName) {
        try {
            // Create a ConnectionFactory
            ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://localhost:61616");

            // Create a Connection
            Connection connection = connectionFactory.createConnection();
            connection.start();

            connection.setExceptionListener(this::exceptionThrown);

            // Create a Session
            Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

            // Create the destination (Topic or Queue)
            Destination destination = session.createQueue(queueName);

            // Create a MessageConsumer from the Session to the Topic or Queue
            MessageConsumer consumer = session.createConsumer(destination);

            // Wait for a message

            Message message;
            for(;;) {
                message = consumer.receive(1000);
                if (message == null) {
                    break;
                }
                if (message instanceof TextMessage) {
                    TextMessage textMessage = (TextMessage) message;
                    String text = textMessage.getText();
                    var step = Util.deserializeStep(text);
                    //System.out.println(Util.StepToString(step));
                } else {
                    System.out.println("lol");
                    var step = Util.deserializeStep(message.toString());
                    //System.out.println(Util.StepToString(step));
                }
            }


            consumer.close();
            session.close();
            connection.close();
        } catch (Exception e) {
            System.out.println("Caught: " + e);
            e.printStackTrace();
        }
    }


    @Override
    public void exceptionThrown(Exception e) {
        System.out.println("JMS Exception occured.  Shutting down client.");
    }
}