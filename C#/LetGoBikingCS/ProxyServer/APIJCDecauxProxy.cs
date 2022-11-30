using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace ProxyServer
{
    internal class ApijcDecauxProxy : IAPIJCDecauxProxy
    {
        private const string ApiKey = "b695836afb4b0f2d7df5b107c46f4c2f190fcffc";
        private const string BaseUri = "https://api.jcdecaux.com/vls/v3/";
        private static readonly string KeyUri = $"apiKey={ApiKey}";

        private static readonly GenericProxyCache<List<Contract>> ContractsCache = new GenericProxyCache<List<Contract>>();
        private static readonly GenericProxyCache<Station> StationCache = new GenericProxyCache<Station>();
        private static readonly GenericProxyCache<List<Station>> StationsCache = new GenericProxyCache<List<Station>>();

        public List<Contract> Contracts()
        {
            var reqString = BaseUri + "contracts" + "?" + KeyUri;
            var res = ContractsCache.Get(reqString);
            res.RemoveAll(x => x.cities == null);
            //res.ForEach(c => c.cities = c.cities ?? new List<string> { c.name.First().ToString().ToUpper() + c.name.Substring(1) });
            //res.RemoveAll(x => x.name == "jcdecauxbike");
            return res;
        }

        public Station StationOfContract(string contractName, int stationNumber)
        {
            var reqString = BaseUri + "stations/" + stationNumber + "?contract=" + contractName + "&" + KeyUri;
            return StationCache.Get(reqString, 5 * 60);
        }

        public Station ClosestStation(GeoCoordinate originCoord, string contractName)
        {
            var stations = StationsOfContract(contractName);
            var closestStation = stations[0];


            var minDistance = originCoord.GetDistanceTo(new GeoCoordinate(closestStation.position.latitude,
                closestStation.position.longitude));

            foreach (var station in stations)
            {
                if (station.totalStands.availabilities.stands == 0) continue;
                var distance =
                    originCoord.GetDistanceTo(new GeoCoordinate(station.position.latitude, station.position.longitude));
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestStation = station;
            }

            return closestStation;
        }

        public List<Station> StationsOfContract(string contractName)
        {
            var reqString = BaseUri + "stations?contract=" + contractName + "&" + KeyUri;
            return StationsCache.Get(reqString, 1 * 60);
        }
    }
}