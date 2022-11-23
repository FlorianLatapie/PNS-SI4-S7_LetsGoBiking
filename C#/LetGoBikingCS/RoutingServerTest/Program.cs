using RoutingServerTest.ServiceReference1;
using System;

namespace RoutingServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new RoutingCalculatorClient();
            Console.WriteLine(test.GetItinerary("addr:chemin des caillades", "coord:1.1;2.2"));
            Console.ReadLine();
        }
    }
}