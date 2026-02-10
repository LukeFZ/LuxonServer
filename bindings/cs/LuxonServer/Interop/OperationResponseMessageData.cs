using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct OperationResponseMessageData
{
    public byte OperationCode;
    public short ReturnCode;
    public byte* DebugMessage;
    public byte* SerializedParameters;
    public nuint SerializedParametersSize;
}