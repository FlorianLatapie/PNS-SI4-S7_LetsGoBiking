using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace RoutingServer
{
    public class OpenRouteService
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string ApiUrl = "https://api.openrouteservice.org/v2/directions/";
        private const string Cycling = "cycling-regular/";
        private const string Walking = "foot-walking/";
        private const string ApiKey = "?api_key=5b3ce3597851110001cf62485ee6e70b50c0487cbe89c8f67d924fd6";
        
        private OpenRouteServiceRoot Directions(string type, GeoCoordinate origin, GeoCoordinate destination)
        {
            var originStr = Converter.TupleStrFromGeoCoordinate(origin);
            var destinationStr = Converter.TupleStrFromGeoCoordinate(destination);
            
            var requestUrl = ApiUrl + type + ApiKey + "&start=" + originStr.Item2 + "," + originStr.Item1 + "&end=" + destinationStr.Item2 + "," + destinationStr.Item1;
            Console.WriteLine($"Api call to OpenRouteService {requestUrl}");

            var openRouteServiceResponseBody = Client.GetStringAsync(requestUrl).Result;
            return JsonSerializer.Deserialize<OpenRouteServiceRoot>(openRouteServiceResponseBody);
        }
        
        public OpenRouteServiceRoot DirectionsWalking(GeoCoordinate originCoord, GeoCoordinate destinationCoord)
        {
            return Directions(Walking, originCoord, destinationCoord); 
        }
        
        public OpenRouteServiceRoot DirectionsCycling(GeoCoordinate originCoord, GeoCoordinate destinationCoord)
        {
            return Directions(Cycling, originCoord, destinationCoord); 
        }
    }
    
    // OpenRouteServiceRoot myDeserializedClass = JsonConvert.DeserializeObject<OpenRouteServiceRoot>(myJsonResponse);
    public class Engine
    {
        public string version { get; set; }
        public DateTime build_date { get; set; }
        public DateTime graph_date { get; set; }
    }

    public class Feature
    {
        public List<double> bbox { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public List<List<double>> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Metadata
    {
        public string attribution { get; set; }
        public string service { get; set; }
        public long timestamp { get; set; }
        public Query query { get; set; }
        public Engine engine { get; set; }
    }

    public class Properties
    {
        public List<Segment> segments { get; set; }
        public Summary summary { get; set; }
        public List<int> way_points { get; set; }
    }

    public class Query
    {
        public List<List<double>> coordinates { get; set; }
        public string profile { get; set; }
        public string format { get; set; }
    }

    public class OpenRouteServiceRoot
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
        public List<double> bbox { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public int type { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }
        public List<int> way_points { get; set; }
    }

    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }
}