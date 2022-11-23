using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text.Json;

namespace ProxyServer
{
    public class GenericProxyCache<T> where T : class
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly DateTimeOffset _dtDefaultDateTimeOffset = ObjectCache.InfiniteAbsoluteExpiration;

        private Dictionary<string, Tuple<T, DateTimeOffset>>
            _cache = new Dictionary<string, Tuple<T, DateTimeOffset>>();


        public T Get(string cacheItemName, DateTimeOffset dt)
        {
            UpdateCache();

            if (_cache.ContainsKey(cacheItemName) && _cache[cacheItemName].Item1 != null)
            {
                Console.WriteLine($"Utilisation du cache {cacheItemName}");
                return _cache[cacheItemName].Item1;
            }

            Console.WriteLine($"requete vers serveur {cacheItemName}");

            var jcDecauxResponseBody = Client.GetStringAsync(cacheItemName);
            var objectToAdd = JsonSerializer.Deserialize<T>(jcDecauxResponseBody.Result);

            _cache[cacheItemName] = Tuple.Create(objectToAdd, dt);

            return _cache[cacheItemName].Item1;
        }

        public T Get(string cacheItemName, double dtSeconds)
        {
            return Get(cacheItemName, DateTimeOffset.Now.AddSeconds(dtSeconds));
        }

        public T Get(string cacheItemName)
        {
            return Get(cacheItemName, _dtDefaultDateTimeOffset);
        }

        private void UpdateCache()
        {
            //remove all expired items from the cache
            _cache = _cache
                .Where(x => x.Value.Item2 > DateTime.Now)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}