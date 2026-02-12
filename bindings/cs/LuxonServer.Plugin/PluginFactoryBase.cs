namespace LuxonServer.Plugin;

public abstract class PluginFactoryBase : IPluginFactory2
{
    protected IFactoryHost factoryHost = null!;

    public IGamePlugin? Create(IPluginHost gameHost, string pluginName, Dictionary<string, string> config,
        out string errorMessage)
    {
        throw new NotImplementedException();
    }

    public void SetFactoryHost(IFactoryHost fHost, FactoryParams factoryParams)
    {
        factoryHost = fHost;
    }

    public abstract IGamePlugin? CreatePlugin(string pluginName);
}
