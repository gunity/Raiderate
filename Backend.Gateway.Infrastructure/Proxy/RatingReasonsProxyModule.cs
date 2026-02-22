using Backend.Gateway.Infrastructure.Options;
using Backend.Gateway.Infrastructure.Proxy.Common;
using Microsoft.Extensions.Options;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy;

public sealed class RatingReasonsProxyModule(IOptions<DestinationOptions> destinationOptions) : IProxyModule
{
    public RouteConfig Route { get; } = new()
    {
        RouteId = "rating-reasons",
        ClusterId = "rating-reasons",
        Match = new RouteMatch { Path = "/api/rating-reasons/{**catch-all}"}
    };

    public ClusterConfig Cluster { get; } = new()
    {
        ClusterId = "rating-reasons",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["default"] = new() { Address = destinationOptions.Value.RatingReasonsUrl }
        }
    };
}