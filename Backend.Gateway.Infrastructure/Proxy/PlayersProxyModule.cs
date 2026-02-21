using Backend.Gateway.Infrastructure.Options;
using Backend.Gateway.Infrastructure.Proxy.Common;
using Microsoft.Extensions.Options;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy;

public class PlayersProxyModule(IOptions<DestinationOptions> destinationOptions) : IProxyModule
{
    public RouteConfig Route { get; } = new()
    {
        RouteId = "players",
        ClusterId = "players",
        Match = new RouteMatch { Path = "/api/players/{**catch-all}"}
    };

    public ClusterConfig Cluster { get; } = new()
    {
        ClusterId = "players",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["default"] = new() { Address = destinationOptions.Value.PlayersUrl }
        }
    };
}