using System.Collections.Generic;

namespace RoutingServer
{
    public class OpenStreetMapAdressInfo
    {
        public int place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public long osm_id { get; set; }
        public List<string> boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        public int place_rank { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public override string ToString()
        {
            return Util.MyToString(this);
        }
    }

    public class Address
    {
        public string road { get; set; }
        public string hamlet { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string village { get; set; }
        public string municipality { get; set; }
        public string county { get; set; }
        public string ISO31662lvl6 { get; set; }
        public string state { get; set; }
        public string ISO31662lvl4 { get; set; }
        public string region { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }

        public string display()
        {
            return $"" +
                $"{road} " +
                $"{hamlet} " +
                $"{suburb} " +
                $"{city} " +
                $"{village} " +
                $"{municipality} " +
                $"{county} " +
                $"{ISO31662lvl6} " +
                $"{state} " +
                $"{ISO31662lvl4} " +
                $"{region} " +
                $"{postcode} " +
                $"{country} " +
                $"{country_code} ";
        }
        public override string ToString()
        {
            return Util.MyToString(this);
        }

        public string GetCity()
        {
            if (!string.IsNullOrEmpty(city))
                return city;
            if (!string.IsNullOrEmpty(village))
                return village;
            if (!string.IsNullOrEmpty(municipality))
                return municipality;
            return "get city error";
        }
    }

    public class OpenStreetMapCoordInfo
    {
        public int place_id { get; set; }
        public string licence { get; set; }

        public string osm_type { get; set; }

        //public int osm_id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public int place_rank { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public double importance { get; set; }
        public string addresstype { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public Address address { get; set; }
        public List<string> boundingbox { get; set; }
        
        public override string ToString()
        {
            return Util.MyToString(this);
        }
    }
}