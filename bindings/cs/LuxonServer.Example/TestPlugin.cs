namespace LuxonServer.Example;

public sealed class TestPlugin : IPlugin
{
    public TestPlugin()
    {
        Console.WriteLine("TestPlugin: created");
    }

    public PluginResult OnAttach()
    {
        Console.WriteLine("TestPlugin: OnAttach");
        return PluginResult.Continue;
    }

    public PluginResult OnCreateGame()
    {
        Console.WriteLine("TestPlugin: OnCreateGame");
        return PluginResult.Continue;
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing TestPlugin");
    }
}