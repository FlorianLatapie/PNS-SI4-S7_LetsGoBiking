package org.example;

import com.soap.ws.client.generated.ConverterReturnItem;
import com.soap.ws.client.generated.RoutingCalculator;

import javax.swing.*;
import java.io.Console;
import java.util.logging.Level;
import java.util.logging.Logger;

public class App {
    public static void main(String[] args) {
        // setup app ---------------------------------------------------------------------------------------------------

        var watch = System.currentTimeMillis();
        System.out.println("Start");

        RoutingCalculator server;
        try {
            server = new RoutingCalculator();
        } catch (Exception e) {
            System.err.println("Cannot connect to server, make sure it is running and that you have an internet connection");
            return;
        }
        var routingCalculator = server.getBasicHttpBindingIRoutingCalculator();

        // input data
        // var res = routingCalculator.getItinerary("addr:place du général de gaulle rouen", "addr:rue du rem martainville rouen");
        // var res = routingCalculator.getItinerary("addr:place du général de gaulle rouen", "addr:place de la mairie lyon");
        //var res = routingCalculator.getItinerary("addr:rue pelisson villeurbanne", "addr:rue tronchet lyon");

        var origin = "addr:rue pelisson villeurbanne"; var destination = "addr:rue tronchet lyon";
        //var origin = "addr:place du général de gaulle rouen";//var destination = "coord:45.7708222, 4.8578873";

        System.out.println("Origin: " + origin + System.lineSeparator() + "Destination: " + destination + System.lineSeparator());

        // output itinerary --------------------------------------------------------------------------------------------

        ConverterReturnItem res;

        try {
            res = routingCalculator.getItinerary(origin, destination);
        } catch (Exception e) {
            System.err.println("Make sure that the origin and destination are valid");
            return;
        }

        if (!res.isSuccess()) {
            System.err.println(res.getErrorMessage().getValue());
            return;
        }

        System.out.println(res.getQueueName().getValue());
        var consumer = new InstructionsConsumer(res.getQueueName().getValue());
        consumer.run();

        // we are sure that the itinerary is valid
        for (var i = 0; i < res.getItineraries().getValue().getOpenRouteServiceRoot().size(); i++) {

            var openRouteServiceRoot = res.getItineraries().getValue().getOpenRouteServiceRoot().get(i);
            var duration = openRouteServiceRoot.getFeatures().getValue().getFeature().get(0).getProperties().getValue()
                    .getSegments().getValue().getSegment().get(0).getDuration();

            System.out.println((i % 2 == 0 ? "Foot" : "Bike") + ": " + ((int)(duration / 60) == 1 ? "1 minute" : ((int)(duration / 60) + " minutes")));

            for (var step : openRouteServiceRoot.getFeatures().getValue().getFeature().get(0).getProperties().getValue()
                    .getSegments().getValue().getSegment().get(0).getSteps().getValue().getStep()) {
                System.out.println("- " + step.getInstruction().getValue());
            }
            System.out.println();
        }

        watch = System.currentTimeMillis() - watch;
        System.out.println("Time elapsed: " + watch/1000.0 + " seconds");
    }
}
