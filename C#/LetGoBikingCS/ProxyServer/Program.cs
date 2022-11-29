using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ProxyServer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var httpUrl = new Uri("http://localhost:8090/MyService/APIJCDecauxProxy");
            
            var host = new ServiceHost(typeof(ApijcDecauxProxy), httpUrl);

            host.AddServiceEndpoint(typeof(IAPIJCDecauxProxy), new WSHttpBinding(), "");
            
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(smb);
            
            host.Open();

            Console.WriteLine("PROXY SERVER");
            Console.WriteLine("Service is host at " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine("Host is running... Press <Enter> key to stop" + Environment.NewLine);
            Console.ReadLine();
        }
    }
}