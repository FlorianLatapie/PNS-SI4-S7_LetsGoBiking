using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer
{
    class APIJCDecauxProxy : IAPIJCDecauxProxy
    {
        public List<Contract> contracts()
        {
            throw new NotImplementedException();
        }

        public Station stationOfContract(string contractName, int stationNumber)
        {
            throw new NotImplementedException();
        }

        public List<Station> stationsOfContract(string contractName)
        {
            throw new NotImplementedException();
        }
    }
}
