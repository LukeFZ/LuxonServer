
using System.Net;
using LuxonServer;
using LuxonServer.Example;

PluginManager.RegisterPlugin<TestPlugin>("TestPlugin");

var context = ServerContext.Create();
context.Configure("NameServer", IPEndPoint.Parse("127.0.0.1:5058"));
context.Configure("MasterServer", IPEndPoint.Parse("127.0.0.1:5055"));
context.Configure("GameServer", IPEndPoint.Parse("127.0.0.1:5056"));

var thread = new Thread(() =>
{
    using (context)
    {
        context.Run();
    }
});
thread.Start();

Console.WriteLine("Press Enter to stop the server...");
Console.ReadLine();
context.Stop();