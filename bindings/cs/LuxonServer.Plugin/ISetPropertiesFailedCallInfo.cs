namespace LuxonServer.Plugin;

public interface ISetPropertiesFailedCallInfo : ITypedCallInfo<ISetPropertiesRequest>
{
    int ActorNr { get; }
}
