
using System.Net;
using LuxonServer;
using LuxonServer.Example;

PluginManager.RegisterPlugin<TestPlugin>("TestPlugin");

var context = ServerContext.Create();
context.Configure("NameServer", IPEndPoint.Parse("0.0.0.0:5058"));
context.Configure("MasterServer", IPEndPoint.Parse("0.0.0.0:5055"));
context.Configure("GameServer", IPEndPoint.Parse("0.0.0.0:5056"));

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