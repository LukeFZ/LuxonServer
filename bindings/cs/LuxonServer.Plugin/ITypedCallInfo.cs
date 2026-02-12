namespace LuxonServer.Plugin;

public interface ITypedCallInfo<out T> : ICallInfo
{
    T Request { get; }
    string UserId { get; }
    string Nickname { get; }
    object? AuthResultsToken { get; }
    Dictionary<string, object> AuthCookie { get; }
}
