// add assembly System.ServiceModel  and using for the corresponding model
using System.ServiceModel;

namespace RoutingServer
{
    [ServiceContract]
    public interface IRoutingCalculator
    {
        [OperationContract]
        string GetItinerary(string origin, string destination);
    }
}
