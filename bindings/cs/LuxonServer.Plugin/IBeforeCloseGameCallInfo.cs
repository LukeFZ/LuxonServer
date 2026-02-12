namespace LuxonServer.Plugin;

public interface IBeforeCloseGameCallInfo : ITypedCallInfo<ICloseRequest>
{
    bool FailedOnCreate { get; }
}
