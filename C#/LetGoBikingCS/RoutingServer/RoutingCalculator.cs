using RoutingServer.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    class RoutingCalculator : IRoutingCalculator
    {
        private APIJCDecauxProxyClient proxy;

        public RoutingCalculator()
        {
            proxy = new APIJCDecauxProxyClient();
        }


        public string GetItinerary(string origin, string destination)
        {
            var contracts = proxy.contracts();
            return "Routing server : not yet implemented !" + "\n" + origin + "\n" + destination + "\n" + contracts[0].name;
        }
    }
}
