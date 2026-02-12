namespace LuxonServer.Plugin;

public interface IPluginFactory
{
    IGamePlugin? Create(IPluginHost gameHost, string pluginName, Dictionary<string, string> config, out string errorMessage);
}
