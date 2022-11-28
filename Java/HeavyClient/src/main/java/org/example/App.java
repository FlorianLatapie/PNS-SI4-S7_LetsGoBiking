package org.example;

import com.soap.ws.client.generated.RoutingCalculator;

public class App {
    public static void main(String[] args) {
        var watch = System.currentTimeMillis();
        System.out.println("Start !");

        var server = new RoutingCalculator();
        var routingCalculator = server.getWSHttpBindingIRoutingCalculator();
        var res = routingCalculator.getItinerary("addr:place du général de gaulle rouen",
                "addr:rue du rem martainville rouen");

        System.out.println(res);
    }
}