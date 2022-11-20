using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private APIJCDecauxProxyClient _proxy;

        public RoutingCalculator()
        {
            _proxy = new APIJCDecauxProxyClient();
        }


        public string GetItinerary(string origin, string destination)
        {
            var contracts = _proxy.Contracts();
            var res = "Routing server : " + "\n" + origin + "\n" + destination + "\n" + contracts[0].name;

            return res;
        }
    }
}
