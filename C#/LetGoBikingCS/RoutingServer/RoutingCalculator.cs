using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly Converter _converter = new Converter();
        private readonly OpenRouteService _openRouteService = new OpenRouteService();

        private readonly APIJCDecauxProxyClient _proxy = new APIJCDecauxProxyClient();

        private readonly Dictionary<string, Contract> _citiesContracts;


        public RoutingCalculator()
        {
            var contracts = _proxy.Contracts();
            _citiesContracts = Converter.ListStringCitiesFromContracts(contracts);
        }

        public string GetItinerary(string origin, string destination)
        {
            var (originCoord, destinationCoord, originAddressInfo, destinationAddressInfo) =
                PrepareInput(origin, destination);

            // Find the JC Decaux contract associated with the given origin/destination.
            if (!AreInSameContract(originAddressInfo, destinationAddressInfo))
                return
                    "Les deux villes ne sont pas dans le même contrat ou l'une des villes n'est pas dans un contrat JC Decaux.";

            // Retrieve all stations of this/those contract(s).
            var contract = _citiesContracts[originAddressInfo.address.GetCity()];
            var stations = _proxy.StationsOfContract(contract.name);

            // Compute the closest from the origin with available bike
            var closestStationFromOrigin = ClosestStation(originCoord, stations);
            var originStationCoord = Converter.CoordFromStation(closestStationFromOrigin);

            // Compute the closest from the destination with available spots to drop bikes.
            var closestStationFromDestination = ClosestStation(destinationCoord, stations);
            var destinationStationCoord = Converter.CoordFromStation(closestStationFromDestination);

            // Compute itineraries by calling an external REST API.

            // 1. try by foot
            var walkItinerary = _openRouteService.DirectionsWalking(originCoord, destinationCoord);

            // 2. try by bike + foot
            // 2.1 get all itineraries necessary 
            var walkToBikeItinerary = _openRouteService.DirectionsWalking(originCoord, originStationCoord); 
            
            var bikeItinerary =
                _openRouteService.DirectionsCycling(originStationCoord, destinationStationCoord);

            var walkToDestinationItinerary = _openRouteService.DirectionsWalking(destinationStationCoord, destinationCoord);

            // 2.2 compute the total itinerary
            // list of 3 itineraries
            var bikeAndWalkItinerary = new List<OpenRouteServiceRoot>
            {
                walkToBikeItinerary,
                bikeItinerary,
                walkToDestinationItinerary
            };

            // compute duration of bike and walk itinerary using a stram 
            var bikeAndWalkDuration = bikeAndWalkItinerary
                .Select(itinerary => itinerary.features[0].properties.summary.duration)
                .Sum();
            
            if (walkItinerary.features[0].properties.summary.duration < bikeAndWalkDuration)
                return "Itinéraire à pied : " + Util.MyToString(walkItinerary);
            //return "Itinéraire à vélo : " + Util.MyToString(bikeAndWalkItinerary);
            return "Coords : " + Environment.NewLine
                               + "Origin : " + Environment.NewLine + originCoord + Environment.NewLine
                               + "First station : " + Environment.NewLine + originStationCoord + Environment.NewLine
                               + "last station : " + Environment.NewLine + destinationStationCoord + Environment.NewLine
                               + "Destination : " + Environment.NewLine + destinationCoord + Environment.NewLine;
        }

        private bool AreInSameContract(OpenStreetMapCoordInfo city1, OpenStreetMapCoordInfo city2)
        {
            Console.WriteLine($"City1 : {Util.MyToString(city1.address)} - City2 : {Util.MyToString(city2.address)}");
            if (!_citiesContracts.ContainsKey(city1.address.GetCity()) ||
                !_citiesContracts.ContainsKey(city2.address.GetCity())) return false;

            var contract1 = _citiesContracts[city1.address.GetCity()];
            var contract2 = _citiesContracts[city2.address.GetCity()];
            Console.WriteLine("Contract1 : " + contract1.name + " Contract2 : " + contract2.name);

            return contract1 == contract2;
        }

        private static Station ClosestStation(GeoCoordinate originCoord, Station[] stations)
        {
            var closestStation = stations[0];
            var minDistance = originCoord.GetDistanceTo(new GeoCoordinate(closestStation.position.latitude,
                closestStation.position.longitude));

            foreach (var station in stations)
            {
                var distance =
                    originCoord.GetDistanceTo(new GeoCoordinate(station.position.latitude, station.position.longitude));
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestStation = station;
            }

            return closestStation;
        }

        private Tuple<GeoCoordinate, GeoCoordinate, OpenStreetMapCoordInfo, OpenStreetMapCoordInfo> PrepareInput(
            string origin, string destination)
        {
            var (originCoord, destinationCoord) = ProcessInput(origin, destination);

            var (originAddressInfo, destinationAddressInfo) = ProcessCoords(originCoord, destinationCoord);

            return new Tuple<GeoCoordinate, GeoCoordinate, OpenStreetMapCoordInfo, OpenStreetMapCoordInfo>(originCoord,
                destinationCoord, originAddressInfo, destinationAddressInfo);
        }

        private Tuple<GeoCoordinate, GeoCoordinate> ProcessInput(string origin, string destination)
        {
            GeoCoordinate originCoord;
            GeoCoordinate destinationCoord;

            if (origin.StartsWith("addr:"))
                // api call to OpenStreetMap
                originCoord =
                    Converter.OpenStreetMapAdressInfoToGeoCoordinate(
                        _converter.OpenStreetMapAddressInfoFromAddress_api(origin));
            else if (origin.StartsWith("coord:"))
                originCoord = Converter.GeoCoordinateFromStringCoord(origin);
            else
                throw new Exception($"Unknown origin format : {origin}" + Environment.NewLine +
                                    "Please use either 'addr:<address>' or 'coord:<X.X>,<Y.Y>'");

            if (destination.StartsWith("addr:"))
                // api call to OpenStreetMap
                destinationCoord =
                    Converter.OpenStreetMapAdressInfoToGeoCoordinate(
                        _converter.OpenStreetMapAddressInfoFromAddress_api(destination));
            else if (destination.StartsWith("coord:"))
                destinationCoord = Converter.GeoCoordinateFromStringCoord(destination);
            else
                throw new Exception($"Unknown destination format : {destination}" + Environment.NewLine +
                                    "Please use either 'addr:<address>' or 'coord:<X.X>,<Y.Y>'");

            return new Tuple<GeoCoordinate, GeoCoordinate>(originCoord, destinationCoord);
        }

        private Tuple<OpenStreetMapCoordInfo, OpenStreetMapCoordInfo> ProcessCoords(GeoCoordinate origin,
            GeoCoordinate destination)
        {
            // api call to OpenStreetMap
            var originAddressInfo = _converter.OpenStreetMapAddressCoordInfoFromCoord_api(origin);
            // api call to OpenStreetMap
            var destinationAddressInfo = _converter.OpenStreetMapAddressCoordInfoFromCoord_api(destination);
            return new Tuple<OpenStreetMapCoordInfo, OpenStreetMapCoordInfo>(originAddressInfo, destinationAddressInfo);
        }
    }
}