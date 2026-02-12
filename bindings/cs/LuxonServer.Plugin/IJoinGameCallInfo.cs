namespace LuxonServer.Plugin;

public interface IJoinGameCallInfo : ITypedCallInfo<IJoinGameRequest>
{
    int ActorNr { get; }
    ProcessJoinParams ProcessJoinParams { get; }
}
