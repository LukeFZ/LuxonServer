namespace LuxonServer.Plugin;

public interface IPluginFactory2 : IPluginFactory
{
    void SetFactoryHost(IFactoryHost factoryHost, FactoryParams factoryParams);
}
