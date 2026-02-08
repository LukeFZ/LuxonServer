namespace LuxonServer.Example;

public sealed class TestPlugin : GamePlugin
{
    public TestPlugin()
    {
        Console.WriteLine("TestPlugin: created");
    }

    public override PluginResult OnAttach()
    {
        Console.WriteLine("TestPlugin: OnAttach");
        return PluginResult.Continue;
    }

    public override PluginResult OnCreateGame()
    {
        Console.WriteLine("TestPlugin: OnCreateGame");
        return PluginResult.Continue;
    }

    public override PluginResult BeforeJoinGame()
    {
        Console.WriteLine("TestPlugin: BeforeJoinGame");
        return PluginResult.Continue;
    }

    public override PluginResult OnJoinGame()
    {
        Console.WriteLine("TestPlugin: OnJoinGame");
        return PluginResult.Continue;
    }

    protected override void Dispose(bool disposing)
    {
        Console.WriteLine("Disposing TestPlugin");
    }
}