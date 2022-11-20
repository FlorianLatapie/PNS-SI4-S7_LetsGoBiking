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

        // we need them to be static because we don't want to create a new instance of HttpClient for each request
        private static readonly GenericProxyCache<List<Contract>> ContractsCache = new GenericProxyCache<List<Contract>>();
        private static readonly GenericProxyCache<Station> StationCache = new GenericProxyCache<Station>();
        private static readonly GenericProxyCache<List<Station>> StationsCache = new GenericProxyCache<List<Station>>();

        public List<Contract> Contracts()
        {
            var reqString = BaseUri + "contracts" + "?" + KeyUri;
            
            var res = ContractsCache.Get(reqString);
           
            // remove all broken contracts
            res.RemoveAll(x => x.cities == null);
           
            return res;
        }

        public Station StationOfContract(string contractName, int stationNumber)
        {
            var reqString = BaseUri + "stations/" + stationNumber + "?contract=" + contractName + "&" + KeyUri;
            return StationCache.Get(reqString, 5 * 60);
        }

        // this method is here to avoid sending a huge list of all stations of a contract to a client 
        public Station ClosestStation(GeoCoordinate originCoord, string contractName, bool lookingForABike)
        {   
            var stations = StationsOfContract(contractName);
            if (stations == null || stations.Count == 0)
            {
                return null; // avoid server crash
            }
            
            var closestStation = stations[0];

            var minDistance = originCoord.GetDistanceTo(new GeoCoordinate(closestStation.position.latitude,
                closestStation.position.longitude));

            foreach (var station in stations)
            {
                // dont take into account stations with 0 available bikes
                if (lookingForABike && station.totalStands.availabilities.bikes == 0) continue;
                if (!lookingForABike && station.totalStands.availabilities.stands == 0) continue;
                
                var distance =
                    originCoord.GetDistanceTo(
                        new GeoCoordinate(station.position.latitude, station.position.longitude));
                
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