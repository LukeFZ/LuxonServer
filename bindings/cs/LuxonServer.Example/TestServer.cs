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

    protected override FunctionResult HandleDisconnect()
    {
        Console.WriteLine("TestServer: HandleDisconnect");
        return base.HandleDisconnect();
    }

    protected override FunctionResult HandleUpdate()
    {
        Console.WriteLine("TestServer: HandleUpdate");
        return base.HandleUpdate();
    }

    protected override FunctionResult HandleSlowUpdate()
    {
        Console.WriteLine("TestServer: HandleSlowUpdate");
        return base.HandleSlowUpdate();
    }
}