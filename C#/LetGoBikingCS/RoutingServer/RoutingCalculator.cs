using System;
using RoutingServer.ServiceReference1;
using System.ComponentModel;
using System.Text;

namespace RoutingServer
{
    internal class RoutingCalculator : IRoutingCalculator
    {
        private readonly APIJCDecauxProxyClient _proxy;

        public RoutingCalculator()
        {
            _proxy = new APIJCDecauxProxyClient();
        }

        private string ToString(object obj)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(obj);
            StringBuilder builder = new StringBuilder();
            foreach (PropertyDescriptor pd in coll)
            {
                if (pd.GetValue(obj) == null)
                {
                    builder.Append($"{pd.Name} : null");
                }
                else if (pd.GetValue(obj).GetType().IsArray)
                {
                    builder.Append($"{pd.Name} : ");
                    Array.ForEach((object[])pd.GetValue(obj), item => builder.Append($"{item.ToString()} "));
                }
                else
                {
                    builder.Append($"{pd.Name} : {pd.GetValue(obj)}");
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public string GetItinerary(string origin, string destination)
        {
            var contracts = _proxy.Contracts();
            var stationsOfFirstContract = _proxy.StationsOfContract(contracts[0].name);
            var firstStation = _proxy.StationOfContract(contracts[0].name, stationsOfFirstContract[0].number);
            var res =
                "Routing server : \n" +
                "origin : " + origin + "\n" +
                "destination : " + destination + "\n" +
                "1 - contracts : " + ToString(contracts) + "\n" +
                "2 - stations : " + ToString(stationsOfFirstContract) + "\n" +
                "2 - stations[0] : " + ToString(stationsOfFirstContract[0]) + "\n" +
                "3 - one station : " + ToString(firstStation) + "\n" +
                "contracts[0].ToString() : " + ToString(contracts[0]) + "\n";
            return res;
        }
    }
}