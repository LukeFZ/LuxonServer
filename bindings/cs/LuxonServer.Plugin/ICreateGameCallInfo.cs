namespace LuxonServer.Plugin;

public interface ICreateGameCallInfo : ITypedCallInfo<IJoinGameRequest>
{
    bool IsJoin { get; }
    bool CreateIfNotExists { get; }
    Dictionary<string, object>? CreateOptions { get; }
}
