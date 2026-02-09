
using System.Net;
using LuxonServer;
using LuxonServer.Example;

PluginManager.RegisterPlugin<TestPlugin>("TestPlugin");

var context = ServerContext.Create();

context.RegisterServer<TestServer>(nameof(TestServer));

context.ConfigureServer("NameServer", IPEndPoint.Parse("127.0.0.1:5058"));
context.ConfigureServer("MasterServer", IPEndPoint.Parse("127.0.0.1:5055"));
context.ConfigureServer("GameServer", IPEndPoint.Parse("127.0.0.1:5056"));
context.ConfigureServer(nameof(TestServer), IPEndPoint.Parse("127.0.0.1:5059"));

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