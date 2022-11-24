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
            var res = test.GetItinerary("addr:place du général de gaulle rouen",
                "addr:rue du rem martainville rouen");

            Console.WriteLine(res);

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
    }
}