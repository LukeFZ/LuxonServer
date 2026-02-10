using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct EventMessageData
{
    public byte EventCode;
    public byte* SerializedParameters;
    public nuint SerializedParametersSize;
}