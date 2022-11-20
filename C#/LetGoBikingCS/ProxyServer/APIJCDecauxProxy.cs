using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace ProxyServer
{
    class APIJCDecauxProxy : IAPIJCDecauxProxy
    {
        private static readonly string apiKey = "b695836afb4b0f2d7df5b107c46f4c2f190fcffc";
        private static readonly string keyURI = $"?apiKey={apiKey}";
        private static readonly string baseURI = "https://api.jcdecaux.com/vls/v3/";
        
        static readonly HttpClient client = new HttpClient();

        public List<Contract> contracts()
        {
            var JCDecauxResponseBody = client.GetStringAsync(baseURI + "contracts" + keyURI);
            return JsonSerializer.Deserialize<List<Contract>>(JCDecauxResponseBody.Result);
        }

        public Station stationOfContract(string contractName, int stationNumber)
        {
            return new Station();
        }

        public List<Station> stationsOfContract(string contractName)
        {
            return new List<Station>() { new Station() };
        }
    }
}
