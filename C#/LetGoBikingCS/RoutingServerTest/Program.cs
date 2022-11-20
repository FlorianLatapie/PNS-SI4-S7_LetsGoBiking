using System;
using System.Diagnostics;
using RoutingServerTest.ServiceReference1;

namespace RoutingServerTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            Console.WriteLine($"Start !");

            var test = new RoutingCalculatorClient();
            //var res = test.GetItinerary("addr:place du général de gaulle rouen", "addr:rue du rem martainville rouen");
            //var res = test.GetItinerary("addr:place du général de gaulle rouen", "addr:place de la mairie lyon");
            //var res = test.GetItinerary("addr:rue pelisson villeurbanne", "addr:rue tronchet lyon");

            //var origin = "addr:rue pelisson villeurbanne";
            //var destination = "addr:rue tronchet lyon";

            //var origin = "addr:Rue du repos besancon";var destination = "addr:Rue charles nodier besancon";

            //var origin = "addr:allée parmentier créteil";var destination = "addr:Impasse des noyers créteil";

            //var origin = "addr:kammakargatan";var destination = "addr:kammakargatan";

            var origin = "addr:1 Rue Pierre Scheringa, 95000 Cergy"; var destination = "addr:Boulevard d'Erkrath 22, 95650 Puiseux-Pontoise";

            Console.WriteLine($"Origin: {origin}");
            Console.WriteLine($"Destination: {destination}");

            var res = test.GetItinerary(origin, destination);

            if (!res.success)
            {
                Console.WriteLine(res.errorMessage);
                Console.ReadLine();
            }

            Console.WriteLine(res.queueName);

            for (var i = 0; i < res.itineraries.Length; i++)
            {
                var openRouteServiceRoot = res.itineraries[i];
                var duration = openRouteServiceRoot.features[0].properties.segments[0].duration;
                Console.WriteLine((i % 2 == 0 ? $"Foot" : $"Bike") +  $": {(int)(duration / 60)} minutes");
                
                
                foreach (var step in openRouteServiceRoot.features[0].properties.segments[0].steps)
                {
                    Console.WriteLine($"- {step.instruction}");
                }
            }

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
    }
}