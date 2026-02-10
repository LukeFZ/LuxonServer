using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct RawMessageData
{
    public byte* Data;
    public nuint Size;
}