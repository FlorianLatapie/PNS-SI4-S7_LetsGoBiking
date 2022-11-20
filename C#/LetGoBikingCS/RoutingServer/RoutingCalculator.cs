using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    class RoutingCalculator : IRoutingCalculator
    {
        public string GetItinerary(string origin, string destination)
        {
            return "Routing server : not yet implemented !" + "\n" + origin + "\n" + destination;
        }
    }
}
