using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace ProxyServer
{
    internal class ApijcDecauxProxy : IAPIJCDecauxProxy
    {
        private const string ApiKey = "b695836afb4b0f2d7df5b107c46f4c2f190fcffc";
        private static readonly string KeyUri = $"?apiKey={ApiKey}";
        private const string BaseUri = "https://api.jcdecaux.com/vls/v3/";

        private static readonly HttpClient Client = new HttpClient();

        public List<Contract> Contracts()
        {
            var jcDecauxResponseBody = Client.GetStringAsync(BaseUri + "contracts" + KeyUri);
            return JsonSerializer.Deserialize<List<Contract>>(jcDecauxResponseBody.Result);
        }

        public Station StationOfContract(string contractName, int stationNumber)
        {
            return new Station();
        }

        public List<Station> StationsOfContract(string contractName)
        {
            return new List<Station>() { new Station() };
        }
    }
}