using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace ProxyServer
{
    public class GenericProxyCache<T> where T : class
    {
        private readonly DateTimeOffset _dtDefaultDateTimeOffset = ObjectCache.InfiniteAbsoluteExpiration;
        private Dictionary<string, Tuple<T, DateTimeOffset>> _cache = new Dictionary<string, Tuple<T, DateTimeOffset>>();
        
        public T Get(string cacheItemName, DateTimeOffset dt)
        {
            UpdateCache();
            
            if (!_cache.ContainsKey(cacheItemName) || _cache[cacheItemName].Item1 == null)
            {
                _cache[cacheItemName] = new Tuple<T, DateTimeOffset>(Activator.CreateInstance<T>(), dt);
            }

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
            _cache = this._cache
                .Where(x => x.Value.Item2 > DateTime.Now)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}