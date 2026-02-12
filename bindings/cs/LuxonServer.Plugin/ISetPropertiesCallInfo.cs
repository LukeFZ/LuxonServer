namespace LuxonServer.Plugin;

public interface ISetPropertiesCallInfo : ITypedCallInfo<ISetPropertiesRequest>
{
    int ActorNr { get; }
}
