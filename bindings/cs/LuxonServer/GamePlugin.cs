namespace LuxonServer;

public abstract class GamePlugin : IDisposable
{
    public abstract PluginResult OnAttach();
    public abstract PluginResult OnCreateGame();

    ~GamePlugin()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {

    }
}