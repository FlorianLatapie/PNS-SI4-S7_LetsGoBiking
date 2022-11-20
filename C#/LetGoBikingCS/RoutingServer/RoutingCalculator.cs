using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly APIJCDecauxProxyClient _proxy;

        public RoutingCalculator()
        {
            _proxy = new APIJCDecauxProxyClient();
        }


        public string GetItinerary(string origin, string destination)
        {
            var contracts = _proxy.Contracts();
            var stationsOfFirstContract = _proxy.StationsOfContract(contracts[0].name);
            var firstStation = _proxy.StationOfContract(contracts[0].name, stationsOfFirstContract[0].number);
            var res =
                "Routing server : \n" +
                "origin : " + origin + "\n" +
                "destination : " + destination + "\n" +
                "1 - contracts : " + contracts + "\n" +
                "2 - stations : " + stationsOfFirstContract + "\n" +
                "2 - stations[0] : " + stationsOfFirstContract[0] + "\n" +
                "3 - one station : " + firstStation.ToString() + "\n";
            return res;
        }
    }
}