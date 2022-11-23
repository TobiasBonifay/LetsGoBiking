using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Proxy.JCDClasses;

namespace Proxy
{
    internal class ProxyCache : GenericProxyCache<JCDecauxItem>
    {
        public ProxyCache() : base()
        {
        }

        public ProxyCache(DateTimeOffset dtDefault) : base(dtDefault)
        {
        }

        public static void test()
        {
            Task t = JCD.InitializeConnexion();
            t.Wait();

            GenericProxyCache<JCDecauxItem> cache = new GenericProxyCache<JCDecauxItem>();

            string contracts = JCD.GetContracts();
            foreach (var name in contracts.Split('\n'))
            {
                cache.Add(new CacheItem(name), new CacheItemPolicy());
            }
        }
    }
}