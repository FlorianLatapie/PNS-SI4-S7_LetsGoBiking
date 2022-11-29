using System.ServiceModel;
using static RoutingServer.Converter;

namespace RoutingServer
{
    [ServiceContract]
    public interface IRoutingCalculator
    {
        [OperationContract]
        ReturnItem GetItinerary(string origin, string destination);
    }
}