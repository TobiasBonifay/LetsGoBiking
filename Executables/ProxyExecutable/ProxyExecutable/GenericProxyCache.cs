using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyExecutable
{
    internal class GenericProxyCache<T> : ObjectCache, IDisposable
    {
        private readonly DateTimeOffset _dtDefault;

        private readonly Dictionary<string, T> _cache = new Dictionary<string, T>();

        public override DefaultCacheCapabilities DefaultCacheCapabilities { get; }

        public override string Name { get; }

        public GenericProxyCache()
        {
            _dtDefault = InfiniteAbsoluteExpiration;
        }

        public GenericProxyCache(DateTimeOffset dtDefault)
        {
            _dtDefault = dtDefault;
        }


        public T Get<T>(string cacheItemName)
        {
            return Get<T>(cacheItemName, _dtDefault);
        }

        public T Get<T>(string cacheItemName, double dtSeconds)
        {
            return Get<T>(cacheItemName, DateTimeOffset.Now.AddSeconds(dtSeconds));
        }

        public T Get<T>(string cacheItemName, DateTimeOffset dt)
        {
            ObjectCache cache = MemoryCache.Default;
            T response = (T)cache[cacheItemName];
            if (response != null) return response;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = dt;

            response = JsonSerializer.Deserialize<T>(JCD.GetCall(cacheItemName).Result);
            cache.Set(cacheItemName, response, policy);
            return response;
        }

        public override CacheEntryChangeMonitor CreateCacheEntryChangeMonitor(IEnumerable<string> keys,
            string regionName = null)
        {
            throw new NotImplementedException("sorry x)");
        }

        protected override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _cache.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)).GetEnumerator();
        }

        public override bool Contains(string key, string regionName = null)
        {
            return _cache.ContainsKey(key);
        }

        public override object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration,
            string regionName = null)
        {
            if (Contains(key))
                return _cache[key];

            if (Add(key, value, absoluteExpiration, regionName))
                return value;
            throw new Exception("Could not add item to cache");
        }

        public override CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy)
        {
            if (Contains(value.Key))

                return new CacheItem(value.Key, _cache[value.Key]);


            var item = new CacheItem(value.Key, value.Value);
            if (Add(item, policy))
                return item;

            throw new Exception("Could not add item to cache");
        }

        public override object AddOrGetExisting(string key, object value, CacheItemPolicy policy,
            string regionName = null)
        {
            if (Contains(key))
                return _cache[key];
            var item = new CacheItem(key, value);
            if (Add(item, policy))
                return value;
            throw new Exception("Could not add item to cache");
        }

        public override object Get(string key, string regionName = null)
        {
            if (Contains(key))
                return _cache[key];
            return null;
        }

        public override CacheItem GetCacheItem(string key, string regionName = null)
        {
            return Contains(key) ? new CacheItem(key, _cache[key]) : null;
        }

        public override void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
        {
            if (Contains(key))
                _cache[key] = (T)value;
            else
                Add(key, value, absoluteExpiration, regionName);
        }

        public override void Set(CacheItem item, CacheItemPolicy policy)
        {
            if (Contains(item.Key))
                _cache[item.Key] = (T)item.Value;
            else
                Add(item, policy);
        }

        public override void Set(string key, object value, CacheItemPolicy policy, string regionName = null)
        {
            if (Contains(key))
                _cache[key] = (T)value;
            else
                Add(key, value, policy, regionName);
        }

        public override IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                if (Contains(key))
                    dictionary.Add(key, _cache[key]);
            }

            return dictionary;
        }

        public override object Remove(string key, string regionName = null)
        {
            if (!Contains(key)) return null;
            var item = _cache[key];
            _cache.Remove(key);
            return item;
        }

        public override long GetCount(string regionName = null)
        {
            return _cache.Count;
        }

        public override object this[string key]
        {
            get => _cache[key];
            set => _cache[key] = (T)value;
        }

        public void Dispose()
        {
            _cache.Clear();
        }
    }
}
