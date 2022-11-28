using System.Collections.Generic;

namespace ProxyServer
{
    internal class ApijcDecauxProxy : IAPIJCDecauxProxy
    {
        private const string ApiKey = "b695836afb4b0f2d7df5b107c46f4c2f190fcffc";
        private const string BaseUri = "https://api.jcdecaux.com/vls/v3/";
        private static readonly string KeyUri = $"apiKey={ApiKey}";

        private readonly GenericProxyCache<List<Contract>> _contractsCache = new GenericProxyCache<List<Contract>>();
        private readonly GenericProxyCache<Station> _stationCache = new GenericProxyCache<Station>();
        private readonly GenericProxyCache<List<Station>> _stationsCache = new GenericProxyCache<List<Station>>();

        public List<Contract> Contracts()
        {
            var reqString = BaseUri + "contracts" + "?" + KeyUri;
            var res = _contractsCache.Get(reqString);
            res.RemoveAll(x => x.cities == null);
            return res;
        }

        public Station StationOfContract(string contractName, int stationNumber)
        {
            var reqString = BaseUri + "stations/" + stationNumber + "?contract=" + contractName + "&" + KeyUri;
            return _stationCache.Get(reqString, 5 * 60);
        }

        public List<Station> StationsOfContract(string contractName)
        {
            var reqString = BaseUri + "stations?contract=" + contractName + "&" + KeyUri;
            return _stationsCache.Get(reqString, 1 * 60);
        }
    }
}