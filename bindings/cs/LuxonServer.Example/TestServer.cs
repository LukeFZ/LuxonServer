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

    protected override FunctionResult HandleOperationRequest(OperationRequestMessage message, bool isEncrypted, in EnetCommandHeader header)
    {
        Console.WriteLine($"TestServer: HandleOperationRequest: {message}, {isEncrypted}, {header}");
        Console.WriteLine(Peer);

        if (message.OperationCode == 220)
        {
            Console.WriteLine("Answering GetRegions");

            var resp = new OperationResponseMessage(220, 0, new Dictionary<byte, object?>
            {
                [210] = (string[])["eu"],
                [230] = (string[])["127.0.0.1:5055"]
            });

            Peer.Send(resp, header.ToSendOptions(), isEncrypted);
            return FunctionResult.Continue;
        }

        return base.HandleOperationRequest(message, isEncrypted, in header);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}