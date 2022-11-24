using System;
using System.Collections.Generic;
using System.Device.Location;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly Converter _converter = new Converter();
        private readonly OpenRouteService _openRouteService = new OpenRouteService();

        private readonly APIJCDecauxProxyClient _proxy = new APIJCDecauxProxyClient();

        private readonly Dictionary<string, Contract> citiesContracts;


        public RoutingCalculator()
        {
            var contracts = _proxy.Contracts();
            citiesContracts = Converter.ListStringCitiesFromContracts(contracts);
        }

        public string GetItinerary(string origin, string destination)
        {
            var preparedInputs = prepareInput(origin, destination);
            var originCoord = preparedInputs.Item1;
            var destinationCoord = preparedInputs.Item2;
            var originAddressInfo = preparedInputs.Item3;
            var destinationAddressInfo = preparedInputs.Item4;

            // Find the JC Decaux contract associated with the given origin/destination.
            if (!areInSameContract(originAddressInfo, destinationAddressInfo))
                return
                    "Les deux villes ne sont pas dans le même contrat ou l'une des villes n'est pas dans un contrat JC Decaux.";
            ;

            // Retrieve all stations of this/those contract(s).
            var contract = citiesContracts[originAddressInfo.address.GetCity()];
            var stations = _proxy.StationsOfContract(contract.name);

            // Compute the closest from the origin with available bike
            var closestStationFromOrigin = ClosestStation(originCoord, stations);

            // Compute the closest from the destination with available spots to drop bikes.
            var closestStationFromDestination = ClosestStation(destinationCoord, stations);

            // Compute itineraries by calling an external REST API.

            // 1. try by foot
            var walkItinerary = _openRouteService.DirectionsWalking(originCoord, destinationCoord);

            // 2. try by bike + foot
            // 2.1 get all itineraries necessary 
            //var walkToBikeItinerary = // todo

            var geoCoordinateOriginStation = new GeoCoordinate(closestStationFromOrigin.position.latitude,
                closestStationFromOrigin.position.longitude);
            var geoCoordinateDestinationStation = new GeoCoordinate(closestStationFromDestination.position.latitude,
                closestStationFromDestination.position.longitude);
            var bikeItinerary =
                _openRouteService.DirectionsCycling(geoCoordinateOriginStation, geoCoordinateDestinationStation);

            //var walkToDestinationItinerary = // todo

            // 2.2 compute the total itinerary
            //var bikeAndWalkItinerary = // todo

            // 3. compare the itineraries and return the best one
            // (Return the instructions to the client.)

            //if (walkItinerary < bikeAndWalkItinerary)
            if (walkItinerary.features[0].properties.summary.duration <
                bikeItinerary.features[0].properties.summary.duration)
                return "Itinéraire à pied : " + walkItinerary;
            //return "Itinéraire à vélo : " + bikeAndWalkItinerary;
            return "Itinéraire à vélo : " + bikeItinerary;

            Console.WriteLine("Closest station from origin : " + Util.ToString(closestStationFromOrigin) +
                              Environment.NewLine + Util.ToString(closestStationFromOrigin.position));
            Console.WriteLine("Closest station from destination : " + Util.ToString(closestStationFromDestination) +
                              Environment.NewLine + Util.ToString(closestStationFromDestination.position));

            return "terminé";
        }

        public bool areInSameContract(OpenStreetMapCoordInfo city1, OpenStreetMapCoordInfo city2)
        {
            // si la clé existe dans le dictionnaire 
            Console.WriteLine($"City1 : {Util.ToString(city1.address)} - City2 : {Util.ToString(city2.address)}");
            if (!citiesContracts.ContainsKey(city1.address.GetCity()) ||
                !citiesContracts.ContainsKey(city2.address.GetCity())) return false;

            var contract1 = citiesContracts[city1.address.GetCity()];
            var contract2 = citiesContracts[city2.address.GetCity()];
            Console.WriteLine("Contract1 : " + contract1.name + " Contract2 : " + contract2.name);

            return contract1 == contract2;
        }

        public Station ClosestStation(GeoCoordinate originCoord, Station[] stations)
        {
            var closestStation = stations[0];
            var minDistance = originCoord.GetDistanceTo(new GeoCoordinate(closestStation.position.latitude,
                closestStation.position.longitude));

            foreach (var station in stations)
            {
                var distance =
                    originCoord.GetDistanceTo(new GeoCoordinate(station.position.latitude, station.position.longitude));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestStation = station;
                }
            }

            return closestStation;
        }

        private Tuple<GeoCoordinate, GeoCoordinate, OpenStreetMapCoordInfo, OpenStreetMapCoordInfo> prepareInput(
            string origin, string destination)
        {
            var tupleCoord = processInput(origin, destination);
            var originCoord = tupleCoord.Item1;
            var destinationCoord = tupleCoord.Item2;

            var tupleOpenStreetMapAdressInfo = processCoords(originCoord, destinationCoord);
            var originAddressInfo = tupleOpenStreetMapAdressInfo.Item1;
            var destinationAddressInfo = tupleOpenStreetMapAdressInfo.Item2;

            return new Tuple<GeoCoordinate, GeoCoordinate, OpenStreetMapCoordInfo, OpenStreetMapCoordInfo>(originCoord,
                destinationCoord, originAddressInfo, destinationAddressInfo);
        }

        private Tuple<GeoCoordinate, GeoCoordinate> processInput(string origin, string destination)
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

        private Tuple<OpenStreetMapCoordInfo, OpenStreetMapCoordInfo> processCoords(GeoCoordinate origin,
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