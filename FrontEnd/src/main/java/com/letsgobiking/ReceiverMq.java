package com.letsgobiking;


import jakarta.jms.*;
import org.apache.qpid.jms.JmsConnectionFactory;

public class ReceiverMq {

    private ReceiverMq() {
        // private constructor
    }

    public static void fetchData() {
        final JmsConnectionFactory factory = new JmsConnectionFactory("amqp://localhost:5672");
        try (jakarta.jms.Connection connection = factory.createConnection("user", "admin")) {
            connection.start();
            try (Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE)) {
                final Destination destination = session.createQueue("ITINERARY");
                consumeMessage(session, destination);
            } catch (JMSException e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private static void consumeMessage(Session session, Destination destination) throws JMSException {
        try (MessageConsumer consumer = session.createConsumer(destination)) {
            long start = System.currentTimeMillis();
            long count = 1;
            long noInfinitLoop = 100000;
            System.out.println("Waiting for messages...");
            while (noInfinitLoop-- > 0) {
                Message msg = consumer.receive();
                if (msg instanceof TextMessage) {
                    TextMessage textMessage = (TextMessage) msg;
                    String body = textMessage.getText();
                    System.out.println("[MQ] " + body);

                    if ("initialized".equals(body)) {
                        System.out.println("Connection established");
                    } else if ("end".equals(body)) {
                        long diff = System.currentTimeMillis() - start;
                        System.out.printf("Received %d in %.2f seconds%n", count, (1.0 * diff / 1000.0));
                        return;
                    } else {
                        if (count == 1) {
                            start = System.currentTimeMillis();
                        } else if (count % 1000 == 0) {
                            System.out.printf("Received %d messages.%n", count);
                        }
                        count++;
                    }

                } else {
                    System.out.println("Unexpected message type: " + msg.getClass());
                }
            }
        }
    }
}
