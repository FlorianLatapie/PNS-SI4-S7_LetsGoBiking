using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add assembly System.ServiceModel  and using for the corresponding model
using System.ServiceModel;

namespace ProxyServer
{

    [ServiceContract()]
    public interface IAPIJCDecauxProxy
    {
        [OperationContract()]
        List<Contract> contracts();

        [OperationContract()]
        List<Station> stationsOfContract(string contractName);

        [OperationContract()]
        Station stationOfContract(string contractName, int stationNumber);
    }


}
