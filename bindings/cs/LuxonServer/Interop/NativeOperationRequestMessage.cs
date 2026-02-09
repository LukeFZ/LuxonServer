using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct NativeOperationRequestMessage
{
    public byte OperationCode;
    public byte* SerializedParameters;
    public nuint SerializedParametersLength;
}