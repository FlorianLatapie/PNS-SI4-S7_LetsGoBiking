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
            // return list with one contract for test
            return new List<Contract>() { new Contract() { name = "Paris", commercial_name = "Paris", country_code = "FR" } };
        }

        public Station stationOfContract(string contractName, int stationNumber)
        {
            // return station for the test 
            return new Station();
        }

        public List<Station> stationsOfContract(string contractName)
        {
            return new List<Station>() { new Station() };
        }
    }
}
