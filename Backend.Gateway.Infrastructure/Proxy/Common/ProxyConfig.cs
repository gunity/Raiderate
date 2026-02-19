using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy.Common;

public class ProxyConfig : IProxyConfig
{
    public IReadOnlyList<RouteConfig> Routes { get; }
    public IReadOnlyList<ClusterConfig> Clusters { get; }
    public IChangeToken ChangeToken { get; }

    public ProxyConfig(IEnumerable<IProxyModule> modules)
    {
        var proxyModules = modules.ToArray();
        Routes = proxyModules.Select(x => x.Route).ToList();
        Clusters = proxyModules.Select(x => x.Cluster).ToList();
        ChangeToken =  new CancellationChangeToken(CancellationToken.None);
    }
}