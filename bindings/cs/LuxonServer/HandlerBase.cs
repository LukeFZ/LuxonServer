using LuxonServer.Models;

namespace LuxonServer;

public abstract partial class HandlerBase : IDisposable
{
    protected Peer Peer { get; private set; } = null!;

    protected internal virtual FunctionResult HandleConnect() => FunctionResult.CallBase;
    protected internal virtual FunctionResult HandleDisconnect() => FunctionResult.CallBase;
    protected internal virtual FunctionResult HandleUpdate() => FunctionResult.CallBase;
    protected internal virtual FunctionResult HandleSlowUpdate() => FunctionResult.CallBase;

    protected internal virtual FunctionResult HandleOperationRequest(OperationRequestMessage message, bool isEncrypted,
        in EnetCommandHeader header) => FunctionResult.CallBase;

    protected virtual void Dispose(bool disposing)
    {

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    internal void SetPeer(Peer peer)
    {
        Peer = peer;
    }
}