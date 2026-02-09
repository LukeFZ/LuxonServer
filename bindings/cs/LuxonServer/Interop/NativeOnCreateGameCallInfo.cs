using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct NativeOnCreateGameCallInfo
{
    public byte IsJoin;
    public byte CreateIfNotExist;
}