namespace LuxonServer.Plugin;

public interface ICloseGameCallInfo : ITypedCallInfo<ICloseRequest>
{
    int ActorCount { get; }
    bool FailedOnCreate { get; }

    void Defer();
}
