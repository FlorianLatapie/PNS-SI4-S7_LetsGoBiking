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
            Console.WriteLine(test.GetItinerary("addr:chemin des caillades", "coord:48.1;2.2"));

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
    }
}