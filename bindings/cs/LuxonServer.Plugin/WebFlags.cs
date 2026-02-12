namespace LuxonServer.Plugin;

[Flags]
public enum WebFlags : byte
{
    HttpForward = 1 << 0,
    SendAuthCookie = 1 << 1,
    SendSync = 1 << 2,
    SendState = 1 << 3,

    HttpForwardWithAuthCookie = HttpForward | SendAuthCookie
}
