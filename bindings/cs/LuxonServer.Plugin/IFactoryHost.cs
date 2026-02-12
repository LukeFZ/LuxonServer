namespace LuxonServer.Plugin;

public interface IFactoryHost
{
    IPluginFiber CreateFiber();
    IPluginLogger CreateLogger(string loggerName);
}
