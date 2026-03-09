using Backend.Gateway.Infrastructure.Options;
using Backend.Gateway.Infrastructure.Proxy.Common;
using Microsoft.Extensions.Options;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy;

public sealed class VotesProxyModule(IOptions<DestinationOptions> destinationOptions) : IProxyModule
{
    public RouteConfig Route { get; } = new()
    {
        RouteId = "votes",
        ClusterId = "votes",
        Match = new RouteMatch { Path = "/api/votes/{**catch-all}"}
    };

    public ClusterConfig Cluster { get; } = new()
    {
        ClusterId = "votes",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["default"] = new() { Address = destinationOptions.Value.VotesUrl }
        }
    };
}