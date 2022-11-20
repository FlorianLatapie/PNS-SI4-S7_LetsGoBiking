using RoutingServerTest.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
