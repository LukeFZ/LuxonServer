namespace LuxonServer.Plugin;

public interface IRaiseEventCallInfo : ITypedCallInfo<IRaiseEventRequest>
{
    int ActorNr { get; }

    void Cancel();
    void Defer();
}
