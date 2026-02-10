using System.Runtime.InteropServices;
using LuxonServer.Models;

namespace LuxonServer.Interop;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SendMessageArguments
{
    public EnetSendOptions Options;
    public MessageType Type;
    public MessageFlags Flags;
    public void* MessageArguments; // One of: {Raw, Event, OperationResponse}MessageData*
}