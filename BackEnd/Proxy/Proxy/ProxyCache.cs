
namespace Proxy
{
    internal class ProxyCache
    {
        private static ProxyCache instance;
        private static readonly object padlock = new object();

        private ProxyCache()
        {
        }

        public static ProxyCache Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ProxyCache();
                    }
                    return instance;
                }
            }
        }


    }
}
