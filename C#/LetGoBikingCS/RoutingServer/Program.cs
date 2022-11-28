using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Description;

// add the WCF ServiceModel namespace 

namespace RoutingServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            //Be careful to run Visual Studio as Admistrator or to allow VS to open new port netsh command. 
            // Example : netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
            var httpUrl = new Uri("http://localhost:8090/MyService/RoutingCalculator");

            //Create ServiceHost
            //var host = new ServiceHost(typeof(RoutingCalculator), httpUrl);

            // Multiple end points can be added to the Service using AddServiceEndpoint() method.
            // Host.Open() will run the service, so that it can be used by any client.

            // Example adding :
             Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/RoutingCalculator");
             ServiceHost host = new ServiceHost(typeof(RoutingServer.RoutingCalculator), httpUrl, tcpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IRoutingCalculator), new WSHttpBinding(), "");

            //Enable metadata exchange
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("ROUTING SERVER");
            Console.WriteLine("Service is host at " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
    }
}