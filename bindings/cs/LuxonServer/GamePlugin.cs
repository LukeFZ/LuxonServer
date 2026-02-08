namespace LuxonServer;

public abstract class GamePlugin : IDisposable
{
    public abstract PluginResult OnAttach();
    public abstract PluginResult OnCreateGame();
    public abstract PluginResult BeforeJoinGame();
    public abstract PluginResult OnJoinGame();

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