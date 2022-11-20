using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add assembly System.ServiceModel  and using for the corresponding model
using System.ServiceModel;

namespace RoutingServer
{
    [ServiceContract()]
    public interface IRoutingCalculator
    {
        [OperationContract()]
        string GetItinerary(string origin, string destination);
    }
}
