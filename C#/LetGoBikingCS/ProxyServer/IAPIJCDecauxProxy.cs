using System.Collections.Generic;
// add assembly System.ServiceModel  and using for the corresponding model
using System.ServiceModel;

namespace ProxyServer
{
    [ServiceContract]
    public interface IAPIJCDecauxProxy
    {
        [OperationContract]
        List<Contract> Contracts();

        [OperationContract]
        List<Station> StationsOfContract(string contractName);

        [OperationContract]
        Station StationOfContract(string contractName, int stationNumber);
    }
}