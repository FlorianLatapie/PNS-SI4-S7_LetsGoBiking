using System;
using RoutingServer.ServiceReference1;
using System.ComponentModel;
using System.Text;
using System.Device.Location;
using System.Net.Http;
using System.Text.Json;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly APIJCDecauxProxyClient _proxy = new APIJCDecauxProxyClient();
        private readonly Converter _converter = new Converter();
        
        private static readonly HttpClient Client = new HttpClient();
        
        public RoutingCalculator()
        {
            // add user agent to http client
            Client.DefaultRequestHeaders.Add("User-Agent", "RoutingServer");
        }

        private static GeoCoordinate ClosestContract(GeoCoordinate origin, GeoCoordinate destination,
            Contract[] contracts)
        {
            return null; 
        }
        public string GetItinerary(string origin, string destination)
        {
            var tmp = prepareInput(origin, destination);
            var originCoord = tmp.Item1;
            var destinationCoord = tmp.Item2; 
            var originAddressInfo = tmp.Item3;
            var destinationAddressInfo = tmp.Item4;
            
            Console.WriteLine($"Origin address info : {Util.ToString(originAddressInfo.address)}");
            Console.WriteLine($"Destination address info : {Util.ToString(destinationAddressInfo.address)}");
            

            /* Find the JC Decaux contract associated with the given origin/destination.
            Retrieve all stations of this/those contract(s).
            Compute the closest from the origin with available bikes.
            Compute the closest from the destination with available spots to drop bikes.
            Check if the closest available stations are close enough so that it is worth to use them compared to directly go on foot from the origin to the destination.
            Compute itineraries by calling an external REST API.
            Return the instructions to the client.
            */
            
            
            return "terminé";
        }
        
        private Tuple<GeoCoordinate, GeoCoordinate,OpenStreetMapCoordInfo,OpenStreetMapCoordInfo> prepareInput(string origin, string destination)
        {
            var tupleCoord = processInput(origin, destination);
            var originCoord = tupleCoord.Item1;
            var destinationCoord = tupleCoord.Item2;
            
            var tupleOpenStreetMapAdressInfo = processCoords(originCoord, destinationCoord);
            var originAddressInfo = tupleOpenStreetMapAdressInfo.Item1;
            var destinationAddressInfo = tupleOpenStreetMapAdressInfo.Item2;
            
            return new Tuple<GeoCoordinate, GeoCoordinate, OpenStreetMapCoordInfo, OpenStreetMapCoordInfo>(originCoord, destinationCoord, originAddressInfo, destinationAddressInfo);
        }

        private Tuple<GeoCoordinate, GeoCoordinate> processInput(string origin, string destination)
        {
            GeoCoordinate originCoord;
            GeoCoordinate destinationCoord;

            if (origin.StartsWith("addr:"))
            {
                // api call to OpenStreetMap
                originCoord = Converter.OpenStreetMapAdressInfoToGeoCoordinate(_converter.OpenStreetMapAddressInfoFromAddress_api(origin));
            }
            else if (origin.StartsWith("coord:"))
            {
                originCoord = Converter.GeoCoordinateFromStringCoord(origin);
            }
            else
            {
                throw new Exception($"Unknown origin format : {origin}");
            }

            if (destination.StartsWith("addr:"))
            {
                // api call to OpenStreetMap
                destinationCoord =  Converter.OpenStreetMapAdressInfoToGeoCoordinate(_converter.OpenStreetMapAddressInfoFromAddress_api(destination));
            }
            else if (destination.StartsWith("coord:"))
            {
                destinationCoord = Converter.GeoCoordinateFromStringCoord(destination);
            }
            else
            {
                throw new Exception($"Unknown destination format : {destination}");
            }

            return new Tuple<GeoCoordinate, GeoCoordinate>(originCoord, destinationCoord);
        }

        private Tuple<OpenStreetMapCoordInfo, OpenStreetMapCoordInfo> processCoords(GeoCoordinate origin, GeoCoordinate destination)
        {
            // api call to OpenStreetMap
            var originAddressInfo = _converter.OpenStreetMapAddressCoordInfoFromCoord_api(origin);
            // api call to OpenStreetMap
            var destinationAddressInfo = _converter.OpenStreetMapAddressCoordInfoFromCoord_api(destination);
            return new Tuple<OpenStreetMapCoordInfo, OpenStreetMapCoordInfo>(originAddressInfo, destinationAddressInfo);
        }
    }
}