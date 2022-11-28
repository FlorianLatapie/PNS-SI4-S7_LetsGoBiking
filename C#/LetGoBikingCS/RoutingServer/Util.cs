using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    public class Util
    {
        public static string MyToString(object myObject)
        {
            // if the object is primitive, just return it
            if (myObject.GetType().IsPrimitive || myObject is string) return myObject.ToString();
            var result = new StringBuilder();
            // if myObject is a list, then we need to iterate through the list and print each item

            if (myObject is IEnumerable enumerable)
            {
                result.Append("[");
                foreach (var item in enumerable) result.Append(MyToString(item)).Append(", ");
                result.Append("]");
                return result.ToString();
            }

            var propertyInfos = myObject.GetType().GetProperties();
            foreach (var property in propertyInfos)
            {
                var value = property.GetValue(myObject);
                if (value != null)
                {
                    if (value.GetType().IsArray)
                    {
                        var array = (object[])value;
                        if (array.Length > 0)
                        {
                            result.Append($"{property.Name} : ");
                            result.Append("[");
                            Array.ForEach(array, item => result.Append($"{MyToString(item)}, "));
                            result.Append("]");
                        }
                    }

                    // if the property is a list (ex:'System.Collections.Generic.List`1[RoutingServer.Feature]'), we need to iterate over the list and print the values
                    else if (value.GetType().IsGenericType &&
                             value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                    {
                        result.Append($"{property.Name} : ");
                        result.Append("[");
                        foreach (var item in (IList)value) result.Append($"{MyToString(item)}, ");
                        result.Append("]");
                    }
                    else
                    {
                        result.Append($"{property.Name} : {value}").AppendLine();
                    }

                    //result.Append(Environment.NewLine);
                }
            }

            return result.ToString();
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

            var openStreetMapResponseBody = Client.GetStringAsync(requestUrl).Result;
            Console.WriteLine(openStreetMapResponseBody);
            return JsonSerializer.Deserialize<OpenStreetMapAdressInfo[]>(openStreetMapResponseBody)[0];
        }

        public OpenStreetMapCoordInfo OpenStreetMapAddressCoordInfoFromCoord_api(GeoCoordinate coord)
        {
            // format : "coord:X.X;Y.Y" 
            var strCoords = TupleStrFromGeoCoordinate(coord);

            var requestUrl =
                $"https://nominatim.openstreetmap.org/reverse.php?lat={strCoords.Item1}&lon={strCoords.Item2}&format=jsonv2";

            var openStreetMapResponseBody = Client.GetStringAsync(requestUrl).Result;
            return JsonSerializer.Deserialize<OpenStreetMapCoordInfo>(openStreetMapResponseBody);
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

        public static Dictionary<string, Contract> ListStringCitiesFromContracts(Contract[] contracts)
        {
            var dict = new Dictionary<string, Contract>();

            foreach (var contract in contracts)
            foreach (var city in contract.cities)
                dict.Add(city, contract);

            return dict;
        }

        public static Tuple<string, string> TupleStrFromGeoCoordinate(GeoCoordinate geoCoordinate)
        {
            var latitude = geoCoordinate.Latitude.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
            var longitude = geoCoordinate.Longitude.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
            return new Tuple<string, string>(latitude, longitude);
        }

        public static GeoCoordinate CoordFromStation(Station station)
        {
            return new GeoCoordinate(station.position.latitude, station.position.longitude);
        }

        public class ReturnItem
        {
            public bool success { get; set; }
            public string errorMessage { get; set; }
            public List<OpenRouteServiceRoot> itineraries { get; set; }
            
            public ReturnItem(string errorMessage)
            {
                this.success = false;
                this.errorMessage = errorMessage;
            }
            
            public ReturnItem(List<OpenRouteServiceRoot> itineraries)
            {
                this.success = true;
                this.itineraries = itineraries;
            }
        }
    }
}