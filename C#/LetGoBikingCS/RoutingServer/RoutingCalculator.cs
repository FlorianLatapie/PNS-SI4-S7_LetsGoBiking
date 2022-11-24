using System;
using System.Collections.Generic;
using System.Device.Location;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly Converter _converter = new Converter();
        private readonly APIJCDecauxProxyClient _proxy = new APIJCDecauxProxyClient();

        private Dictionary<string, Contract> citiesContracts;  
        
        public RoutingCalculator()
        {
            var contracts = _proxy.Contracts();
            citiesContracts = _converter.ListStringCitiesFromContracts(contracts);
        }
        
        public bool areInSameContract(OpenStreetMapCoordInfo city1, OpenStreetMapCoordInfo city2)
        {
            // si la clé existe dans le dictionnaire 
            Console.WriteLine($"City1 : {Util.ToString(city1.address)} - City2 : {Util.ToString(city2.address)}");
            if (!citiesContracts.ContainsKey(city1.address.GetCity()) || !citiesContracts.ContainsKey(city2.address.GetCity()))
            {
                return false;
            }

            var contract1 = citiesContracts[city1.address.GetCity()];
            var contract2 = citiesContracts[city2.address.GetCity()];
            Console.WriteLine("Contract1 : " + contract1.name + " Contract2 : " + contract2.name);
            
            return contract1 == contract2;
        }

        public string GetItinerary(string origin, string destination)
        {
            var preparedInputs = prepareInput(origin, destination);
            var originCoord = preparedInputs.Item1;
            var destinationCoord = preparedInputs.Item2;
            var originAddressInfo = preparedInputs.Item3;
            var destinationAddressInfo = preparedInputs.Item4;

            /* Find the JC Decaux contract associated with the given origin/destination.
            Retrieve all stations of this/those contract(s).
            Compute the closest from the origin with available bikes.
            Compute the closest from the destination with available spots to drop bikes.
            Check if the closest available stations are close enough so that it is worth to use them compared to directly go on foot from the origin to the destination.
            Compute itineraries by calling an external REST API.
            Return the instructions to the client.
            */
            
            // Find the JC Decaux contract associated with the given origin/destination.
            
            if (!areInSameContract(originAddressInfo, destinationAddressInfo))
            {
                return "Les deux villes ne sont pas dans le même contrat";
            };
            
            // Retrieve all stations of this/those contract(s).
            
            var contract  = citiesContracts[originAddressInfo.address.GetCity()];
            
            // Compute the closest from the origin with available bikes.
            
            //var closestStationFromOrigin = closestStation(originCoord, contract);
            
            


            return "terminé";
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