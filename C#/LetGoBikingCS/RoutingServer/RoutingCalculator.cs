using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using RoutingServer.ServiceReference1;
using static RoutingServer.Converter;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly Dictionary<string, Contract> _citiesContracts;
        private readonly Converter _converter = new Converter();
        private readonly OpenRouteService _openRouteService = new OpenRouteService();

        private readonly APIJCDecauxProxyClient _proxy = new APIJCDecauxProxyClient();
        
        public RoutingCalculator()
        {
            var contracts = _proxy.Contracts();
            _citiesContracts = Converter.ListStringCitiesFromContracts(contracts);
        }

        public ReturnItem GetItinerary(string origin, string destination)
        {
            ActiveMQ.run();

            var (originCoord, destinationCoord, originAddressInfo, destinationAddressInfo) =
                PrepareInput(origin, destination);
            
            // Find the JC Decaux contract associated with the given origin/destination.
            if (!IsInJCDContracts(originAddressInfo)) 
                return new ReturnItem($"Origin city is not in JC Decaux contracts: {originAddressInfo.address.GetCity()}");
   
            // Retrieve all stations of this/those contract(s).
            var contract = _citiesContracts[originAddressInfo.address.GetCity()];
            var contractName = contract.name;

            // Try by foot 
            var walkItinerary = _openRouteService.DirectionsWalking(originCoord, destinationCoord);

            // Compute the closest from the origin with available bike
            var closestStationFromOrigin = ClosestStation(originCoord, contractName, true);
            if (closestStationFromOrigin == null)
                return new ReturnItem(new List<OpenRouteServiceRoot> { walkItinerary });
            
            var originStationCoord = CoordFromStation(closestStationFromOrigin);

            // Compute the closest from the destination with available spots to drop bikes.
            var closestStationFromDestination = ClosestStation(destinationCoord, contractName, false);
            var destinationStationCoord = CoordFromStation(closestStationFromDestination);

            // Compute itineraries by calling an external REST API.


            // try by bike + foot
            // 1 get all itineraries necessary 
            var walkToBikeItinerary = _openRouteService.DirectionsWalking(originCoord, originStationCoord);

            var bikeItinerary =
                _openRouteService.DirectionsCycling(originStationCoord, destinationStationCoord);

            var walkToDestinationItinerary =
                _openRouteService.DirectionsWalking(destinationStationCoord, destinationCoord);

            // 2 compute the total itinerary
            // list of 3 itineraries
            var bikeAndWalkItinerary = new List<OpenRouteServiceRoot>
            {
                walkToBikeItinerary,
                bikeItinerary,
                walkToDestinationItinerary
            };

            // compute duration of bike and walk itinerary using a stream 
            var bikeAndWalkDuration = bikeAndWalkItinerary
                .Select(itinerary => itinerary.features[0].properties.summary.duration)
                .Sum();

            // Return the shortest itinerary
            Console.WriteLine("========================================");
            Console.WriteLine($"Origin     : in[{origin}] : {originCoord}, {originAddressInfo.address.display()}");
            Console.WriteLine($"Destination: in[{destination}] : {destinationCoord}, {destinationAddressInfo.address.display()}");
            Console.WriteLine($"{originCoord}");
            Console.WriteLine($"{originStationCoord}");
            Console.WriteLine($"{destinationStationCoord}");
            Console.WriteLine($"{destinationCoord}");
            Console.WriteLine($"========================================" + Environment.NewLine);
            return walkItinerary.features[0].properties.summary.duration < bikeAndWalkDuration ?
                new ReturnItem(new List<OpenRouteServiceRoot> { walkItinerary }) : new ReturnItem(bikeAndWalkItinerary);
        }

        private bool IsInJCDContracts(OpenStreetMapCoordInfo city)
        {
            return _citiesContracts.ContainsKey(city.address.GetCity());
        }

        private Station ClosestStation(GeoCoordinate originCoord, string contractName, bool lookingForABike)
        {
            return _proxy.ClosestStation(originCoord, contractName, lookingForABike);
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
                    Converter.OpenStreetMapAddressInfoToGeoCoordinate(
                        _converter.OpenStreetMapAddressInfoFromAddress_api(origin));
            else if (origin.StartsWith("coord:"))
                originCoord = Converter.GeoCoordinateFromStringCoord(origin);
            else
                throw new Exception($"Unknown origin format : {origin}" + Environment.NewLine +
                                    "Please use either 'addr:<address>' or 'coord:<X.X>,<Y.Y>'");

            if (destination.StartsWith("addr:"))
                // api call to OpenStreetMap
                destinationCoord =
                    Converter.OpenStreetMapAddressInfoToGeoCoordinate(
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