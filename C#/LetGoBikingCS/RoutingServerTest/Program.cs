using RoutingServerTest.ServiceReference1;
using System;

namespace RoutingServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            var test = new RoutingCalculatorClient();
            Console.WriteLine(test.GetItinerary("addr:chemin des caillades", "coord:48.1;2.2"));
            
            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.Elapsed}");
            Console.ReadLine();
        }
    }
}