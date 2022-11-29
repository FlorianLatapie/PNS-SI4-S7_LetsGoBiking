using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace RoutingServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var httpUrl = new Uri("http://localhost:8090/MyService/RoutingCalculator");
            
            var host = new ServiceHost(typeof(RoutingCalculator), httpUrl);
            
            host.AddServiceEndpoint(typeof(IRoutingCalculator), new BasicHttpBinding(), "");

            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(smb);

            host.Open();

            Console.WriteLine("ROUTING SERVER");
            Console.WriteLine("Service is host at " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine("Host is running... Press <Enter> key to stop" + Environment.NewLine);
            Console.ReadLine();
        }
    }
}