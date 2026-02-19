using Yarp.ReverseProxy.Configuration;

namespace Backend.Gateway.Infrastructure.Proxy.Common;

public class ProxyConfigProvider(IEnumerable<IProxyModule> modules) : IProxyConfigProvider
{
    private readonly IProxyConfig _config = new ProxyConfig(modules);
    
    public IProxyConfig GetConfig() => _config;
}