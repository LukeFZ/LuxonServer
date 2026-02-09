namespace LuxonServer.Example;

public class TestServer : HandlerBase
{
    public TestServer()
    {
        Console.WriteLine("TestServer constructed");
    }

    protected override void HandleConnect()
    {
        Console.WriteLine("TestServer: HandleConnect");
        base.HandleConnect();
    }
}