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

            var test = new RoutingCalculatorClient();
            Console.WriteLine(test.GetItinerary("addr:place du général de gaulle rouen", "addr:place saint marc rouen"));

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
    }
}