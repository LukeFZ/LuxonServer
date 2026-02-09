using LuxonServer.Models;

namespace LuxonServer.Example;

public class TestServer : HandlerBase
{
    public TestServer()
    {
        Console.WriteLine("TestServer constructed");
    }

    protected override FunctionResult HandleConnect()
    {
        Console.WriteLine("TestServer: HandleConnect");
        return base.HandleConnect();
    }
}