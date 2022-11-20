using RoutingServerTest.ServiceReference1;
using System;

namespace RoutingServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new RoutingCalculatorClient();
            Console.WriteLine(test.GetItinerary("a", "b"));
            Console.ReadLine();
        }
    }
}