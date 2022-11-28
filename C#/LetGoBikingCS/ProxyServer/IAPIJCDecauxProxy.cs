using System.Collections.Generic;
using System.Device.Location;
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

        [OperationContract]
        Station ClosestStation(GeoCoordinate originCoord, string contractName);
    }
}