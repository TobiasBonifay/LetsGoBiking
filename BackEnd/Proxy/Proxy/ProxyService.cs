using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class ProxyService : IProxyService
    {
        public async Task<string> GetAllContracts()
        {
            string result = await JCD.InitializeConnexion();
            return result;
        }

        public string GetAllStations()
        {
            return JCD.GetAllStations().Result;
        }
    }
}
