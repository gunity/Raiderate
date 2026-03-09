using Backend.Gateway.Infrastructure.Options;
using Backend.Gateway.Infrastructure.Proxy.Common;
using Microsoft.Extensions.Options;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy;

public class IdentityProxyModule(IOptions<DestinationOptions> destinationOptions) : IProxyModule
{
    public RouteConfig Route { get; } = new()
    {
        RouteId = "identity",
        ClusterId = "identity",
        Match = new RouteMatch { Path = "/api/identity/{**catch-all}"}
    };

    public ClusterConfig Cluster { get; } = new()
    {
        ClusterId = "identity",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["default"] = new() { Address = destinationOptions.Value.IdentityUrl }
        }
    };
}