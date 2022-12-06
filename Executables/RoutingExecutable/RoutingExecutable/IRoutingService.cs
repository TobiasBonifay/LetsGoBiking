using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoutingExecutable.ORSClasses
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IRoutingService
    {

        [OperationContract]
        string GetStations();

        [OperationContract]
        string GetWayInstructions(string fromCoords, string startClosesetStztion, string toCoords, string endClosestStation);

        [OperationContract]
        string GetGPSCoordsFromAddress(string address);

        [OperationContract]
        string FindClosestStation(string address);


    }
}
