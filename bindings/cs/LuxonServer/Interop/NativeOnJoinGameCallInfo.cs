using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct NativeOnJoinGameCallInfo
{
    public nint Joiner;
    public byte PublishUserId;
    public byte BroadcastActorProps;
}