using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy.Common;

public interface IProxyModule
{
    RouteConfig Route { get; }
    ClusterConfig Cluster { get; }
}