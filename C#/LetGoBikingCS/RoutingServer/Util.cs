using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    public class Util
    {
        public static string ToString(object myObject)
        {
            var coll = TypeDescriptor.GetProperties(myObject);
            var builder = new StringBuilder();
            foreach (PropertyDescriptor pd in coll)
            {
                if (pd.GetValue(myObject) == null)
                {
                    builder.Append($"{pd.Name} : null");
                }
                else if (pd.GetValue(myObject).GetType().IsArray)
                {
                    builder.Append($"{pd.Name} : ");
                    Array.ForEach((object[])pd.GetValue(myObject), item => builder.Append($"{item} "));
                }
                else
                {
                    builder.Append($"{pd.Name} : {pd.GetValue(myObject)}");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }

    public class Converter
    {
        private static readonly HttpClient Client = new HttpClient();

        public Converter()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "RoutingServer");
        }

        public OpenStreetMapAdressInfo OpenStreetMapAddressInfoFromAddress_api(string address)
        {
            // format : "addr:1 rue de la paix, 75000 Paris"
            address = address.Replace("addr:", "");
            address = address.Replace(" ", "+");

            var requestUrl = $"https://nominatim.openstreetmap.org/search.php?q={address}&format=jsonv2";
            Console.WriteLine($"Api call to OpenStreetMap {requestUrl}");

            var OpenStreetMapResponseBody = Client.GetStringAsync(requestUrl).Result;
            return JsonSerializer.Deserialize<OpenStreetMapAdressInfo[]>(OpenStreetMapResponseBody)[0];
        }

        public OpenStreetMapCoordInfo OpenStreetMapAddressCoordInfoFromCoord_api(GeoCoordinate coord)
        {
            // format : "coord:X.X;Y.Y" 

            var latitude = coord.Latitude.ToString().Replace(",", ".");
            var longitude = coord.Longitude.ToString().Replace(",", ".");

            var requestUrl =
                $"https://nominatim.openstreetmap.org/reverse.php?lat={latitude}&lon={longitude}&format=jsonv2";

            var OpenStreetMapResponseBody = Client.GetStringAsync(requestUrl).Result;
            return JsonSerializer.Deserialize<OpenStreetMapCoordInfo>(OpenStreetMapResponseBody);
        }

        public static GeoCoordinate OpenStreetMapAdressInfoToGeoCoordinate(
            OpenStreetMapAdressInfo openStreetMapAdressInfo)
        {
            var latitude = double.Parse(openStreetMapAdressInfo.lat.Replace(".", ","));
            var longitude = double.Parse(openStreetMapAdressInfo.lon.Replace(".", ","));

            return new GeoCoordinate(latitude, longitude);
        }

        public static GeoCoordinate GeoCoordinateFromStringCoord(string coord)
        {
            // format : "coord:X.X;Y.Y" 

            var coordSplitted = coord.Split(':');
            var coordValues = coordSplitted[1].Split(';');

            var latitude = double.Parse(coordValues[0].Replace(".", ","));
            var longitude = double.Parse(coordValues[1].Replace(".", ","));

            return new GeoCoordinate(latitude, longitude);
        }

        public Dictionary<string,Contract> ListStringCitiesFromContracts(Contract[] contracts)
        {
            var dict = new Dictionary<string, Contract>();
            
            foreach (var contract in contracts)
            {
                foreach (var city in contract.cities)
                {
                    dict.Add(city, contract);
                }
            }
            
            return dict;
        }
    }
}