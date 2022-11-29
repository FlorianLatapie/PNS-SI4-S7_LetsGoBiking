using System;
using System.Diagnostics;
using RoutingServerTest.ServiceReference1;

namespace RoutingServerTest
{
    internal class Program
    {
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
                Console.WriteLine(i % 2 == 0
                    ? $"à pied : {openRouteServiceRoot.features[0].properties.segments[0].duration / 60} minutes"
                    : $"en vélo : {openRouteServiceRoot.features[0].properties.segments[0].duration / 60} minutes");
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