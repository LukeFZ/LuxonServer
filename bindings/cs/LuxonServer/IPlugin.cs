namespace LuxonServer;

public interface IPlugin : IDisposable
{
    PluginResult OnAttach();
    PluginResult OnCreateGame();
}