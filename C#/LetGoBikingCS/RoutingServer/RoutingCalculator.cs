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
        private static readonly HttpClient Client = new HttpClient();
        
        public RoutingCalculator()
        {
            // add user agent to http client
            Client.DefaultRequestHeaders.Add("User-Agent", "RoutingServer");
        }


        private static string ToString(object myObject)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(myObject);
            StringBuilder builder = new StringBuilder();
            foreach (PropertyDescriptor pd in coll)
            {
                if (pd.GetValue(myObject) == null)
                {
                    builder.Append($"{pd.Name} : null");
                }
                else if (pd.GetValue(myObject).GetType().IsArray)
                {
                    builder.Append($"{pd.Name} : ");
                    Array.ForEach((object[])pd.GetValue(myObject), item => builder.Append($"{item.ToString()} "));
                }
                else
                {
                    builder.Append($"{pd.Name} : {pd.GetValue(myObject)}");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        private static GeoCoordinate GetGeoCoordinateFromAddress(string address)
        {    
            // format : "addr:1 rue de la paix, 75000 Paris"
            address = address.Replace("addr:", "");
            address = address.Replace(" ", "+");

            var requestUrl = $"https://nominatim.openstreetmap.org/search.php?q={address}&format=jsonv2";

            var OpenStreetMapResponseBody = Client.GetStringAsync(requestUrl).Result;
            var openStreetMapAdressInfo = JsonSerializer.Deserialize<OpenStreetMapAdressInfo[]>(OpenStreetMapResponseBody);

            var latitude = double.Parse(openStreetMapAdressInfo[0].lat.Replace(".", ","));
            var longitude = double.Parse(openStreetMapAdressInfo[0].lon.Replace(".", ","));
                
            return new GeoCoordinate(latitude, longitude);
        }

        private static GeoCoordinate GetGeoCoordinateFromStringCoord(string coord)
        {
            // format : "coord:X.X;Y.Y" 
            var coordSplitted = coord.Split(':');
            var coordValues = coordSplitted[1].Split(';');
            var latitude = double.Parse(coordValues[0].Replace(".", ","));
            var longitude = double.Parse(coordValues[1].Replace(".", ","));
            return new GeoCoordinate(latitude, longitude);
        }

        public string GetItinerary(string origin, string destination)
        {
            var tupleCoord = processInput(origin, destination);
            
            GeoCoordinate originCoord = tupleCoord.Item1;
            GeoCoordinate destinationCoord = tupleCoord.Item2;
            
            var res = originCoord.Latitude.ToString() + " " + originCoord.Longitude.ToString() + "\n"
                  + destinationCoord.Latitude.ToString() + " " + destinationCoord.Longitude.ToString();

            /*var contracts = _proxy.Contracts();
            var stationsOfFirstContract = _proxy.StationsOfContract(contracts[0].name);
            var firstStation = _proxy.StationOfContract(contracts[0].name, stationsOfFirstContract[0].number);
            var res =
                "Routing server : \n" +
                "origin : " + origin + "\n" +
                "destination : " + destination + "\n" +
                "1 - contracts : ";
            Array.ForEach(contracts, contract => res += contract.name + " ");
            res += "\n" +
                   "2 - stations : ";
            Array.ForEach(stationsOfFirstContract, station => res += station.name + " ");
            res += "\n" +
                   "2 - stations[0] : " + ToString(stationsOfFirstContract[0]) + "\n" +
                   "3 - one station : " + ToString(firstStation) + "\n" +
                   "contracts[0].ToString() : " + ToString(contracts[0]) + "\n";
            
            var contracts2 = _proxy.Contracts();
            res += "\n contracts from cache : "; 
            Array.ForEach(contracts2, contract => res += contract.name + " ");*/

            return res;
        }

        private Tuple<GeoCoordinate, GeoCoordinate> processInput(string origin, string destination)
        {
            GeoCoordinate originCoord;
            GeoCoordinate destinationCoord;

            if (origin.StartsWith("addr:"))
            {
                originCoord = GetGeoCoordinateFromAddress(origin);
            }
            else if (origin.StartsWith("coord:"))
            {
                originCoord = GetGeoCoordinateFromStringCoord(origin);
            }
            else
            {
                throw new Exception($"Unknown origin format : {origin}");
            }

            if (destination.StartsWith("addr:"))
            {
                destinationCoord = GetGeoCoordinateFromAddress(destination);
            }
            else if (destination.StartsWith("coord:"))
            {
                destinationCoord = GetGeoCoordinateFromStringCoord(destination);
            }
            else
            {
                throw new Exception($"Unknown destination format : {destination}");
            }

            return new Tuple<GeoCoordinate, GeoCoordinate>(originCoord, destinationCoord);
        }
    }
}