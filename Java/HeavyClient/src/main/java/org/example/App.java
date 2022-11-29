package org.example;

import com.soap.ws.client.generated.RoutingCalculator;

/* C# :
private static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            Console.WriteLine($"Start !");


            var test = new RoutingCalculatorClient();
            //var res = test.GetItinerary("addr:place du général de gaulle rouen", "addr:rue du rem martainville rouen");
            //var res = test.GetItinerary("addr:place du général de gaulle rouen", "addr:place de la mairie lyon");
            var res = test.GetItinerary("addr:rue pelisson villeurbanne", "addr:rue tronchet lyon");

            for (var i = 0; i < res.itineraries.Length; i++)
            {
                var openRouteServiceRoot = res.itineraries[i];
                var duration = openRouteServiceRoot.features[0].properties.segments[0].duration;
                Console.WriteLine((i % 2 == 0 ? $"Foot" : $"Bike") +  $": {duration / 60} minutes");

                foreach (var step in openRouteServiceRoot.features[0].properties.segments[0].steps)
                {
                    Console.WriteLine($"- {step.instruction}");
                }
            }

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
 */

public class App {
    public static void main(String[] args) {
        var watch = System.currentTimeMillis();
        System.out.println("Start !");

        var server = new RoutingCalculator();
        var routingCalculator = server.getBasicHttpBindingIRoutingCalculator();
        // var res = routingCalculator.getItinerary("addr:place du général de gaulle rouen", "addr:rue du rem martainville rouen");
        // var res = routingCalculator.getItinerary("addr:place du général de gaulle rouen", "addr:place de la mairie lyon");
        var res = routingCalculator.getItinerary("addr:rue pelisson villeurbanne", "addr:rue tronchet lyon");

        for (var i = 0; i < res.getItineraries().getValue().getOpenRouteServiceRoot().size(); i++) {
            var openRouteServiceRoot = res.getItineraries().getValue().getOpenRouteServiceRoot().get(i);
            var duration = openRouteServiceRoot.getFeatures().getValue().getFeature().get(0).getProperties().getValue()
                    .getSegments().getValue().getSegment().get(0).getDuration();
            System.out.println((i % 2 == 0 ? "Foot" : "Bike") + ": " + duration / 60 + " minutes");

            for (var step : openRouteServiceRoot.getFeatures().getValue().getFeature().get(0).getProperties().getValue()
                    .getSegments().getValue().getSegment().get(0).getSteps().getValue().getStep()) {
                System.out.println("- " + step.getInstruction().getValue());
            }
        }

        watch = System.currentTimeMillis() - watch;
        System.out.println("Time elapsed: " + watch/1000.0 + " seconds");
    }
}
