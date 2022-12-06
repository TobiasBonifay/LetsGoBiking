using System;
using System.Threading.Tasks;

// WCF ServiceModel namespace 
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ProxyExecutable
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IProxyService
    {
        [OperationContract]
        Task<string> GetAllContracts();

        [OperationContract]
        string GetAllStations();

        [OperationContract]
        string GetStation(string name);

        [OperationContract]
        string GetStationByContract(string contract);

    }
}
