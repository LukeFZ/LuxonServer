namespace LuxonServer.Plugin;

public interface IDisconnectCallInfo : ITypedCallInfo<IOperationRequest>
{
    int ActorNr { get; }
}
