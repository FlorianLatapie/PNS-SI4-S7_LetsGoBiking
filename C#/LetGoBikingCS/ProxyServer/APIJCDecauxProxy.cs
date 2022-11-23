using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace ProxyServer
{
    internal class ApijcDecauxProxy : IAPIJCDecauxProxy
    {
        private const string ApiKey = "b695836afb4b0f2d7df5b107c46f4c2f190fcffc";
        private static readonly string KeyUri = $"apiKey={ApiKey}";
        private const string BaseUri = "https://api.jcdecaux.com/vls/v3/";

        private GenericProxyCache<List<Contract>> _contractsCache = new GenericProxyCache<List<Contract>>();
        private GenericProxyCache<List<Station>> _stationsCache = new GenericProxyCache<List<Station>>();
        private GenericProxyCache<Station> _stationCache = new GenericProxyCache<Station>();

        public List<Contract> Contracts()
        {
            var reqString = BaseUri + "contracts" + "?" + KeyUri;
            return _contractsCache.Get(reqString);
        }

        public Station StationOfContract(string contractName, int stationNumber)
        {
            var reqString = BaseUri + "stations/" + stationNumber + "?contract=" + contractName + "&" + KeyUri;
            return _stationCache.Get(reqString, 1 * 60);
        }

        public List<Station> StationsOfContract(string contractName)
        {
            var reqString = BaseUri + "stations?contract=" + contractName + "&" + KeyUri;
            return _stationsCache.Get(reqString, 5 * 60);
        }
    }
}