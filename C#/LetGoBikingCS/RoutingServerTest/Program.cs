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
            var test = new SimpleCalculatorClient();
            Console.WriteLine(test.Add(1, 2));
            Console.ReadLine();
        }
    }
}
