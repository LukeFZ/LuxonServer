using System.Runtime.InteropServices;
using LuxonServer.Interop;
using LuxonServer.Models;
using LuxonServer.Serialization;

namespace LuxonServer;

public class Peer
{
    private readonly PeerHandle _handle;

    internal Peer(PeerHandle handle)
    {
        _handle = handle;
    }

    public unsafe void Send(ReadOnlySpan<byte> rawMessage, in EnetSendOptions options)
    {
        fixed (byte* rawMessageBytes = rawMessage)
        {
            var message = new RawMessageData
            {
                Data = rawMessageBytes,
                Size = (nuint)rawMessage.Length
            };

            var arguments = new SendMessageArguments
            {
                Options = options,
                Type = MessageType.RawData,
                Flags = 0,
                MessageArguments = &message
            };

            NativeMethods.luxon_csharp_peer_send_message(_handle, &arguments);
        }
    }

    public unsafe void Send(OperationResponseMessage message, in EnetSendOptions options, bool encrypted)
    {
        var serialized = ValueSerialization.Serialize(message.Parameters);

        var stringPtr = Marshal.StringToCoTaskMemUTF8(message.DebugMessage);

        fixed (byte* serializedBytes = serialized)
        {
            var nativeMessage = new OperationResponseMessageData
            {
                OperationCode = message.OperationCode,
                ReturnCode = message.ReturnCode,
                DebugMessage = (byte*)stringPtr,
                SerializedParameters = serializedBytes,
                SerializedParametersSize = (nuint)serialized.Length
            };

            var arguments = new SendMessageArguments
            {
                Options = options,
                Type = MessageType.OperationResponse,
                Flags = encrypted ? MessageFlags.IsEncrypted : 0,
                MessageArguments = &nativeMessage
            };

            NativeMethods.luxon_csharp_peer_send_message(_handle, &arguments);
            Marshal.FreeCoTaskMem(stringPtr);
        }
    }

    public unsafe void Send(EventMessage message, in EnetSendOptions options, bool encrypted)
    {
        var serialized = ValueSerialization.Serialize(message.Parameters);

        fixed (byte* serializedBytes = serialized)
        {
            var nativeMessage = new EventMessageData
            {
                EventCode = message.EventCode,
                SerializedParameters = serializedBytes,
                SerializedParametersSize = (nuint)serialized.Length
            };

            var arguments = new SendMessageArguments
            {
                Options = options,
                Type = MessageType.Event,
                Flags = encrypted ? MessageFlags.IsEncrypted : 0,
                MessageArguments = &nativeMessage
            };

            NativeMethods.luxon_csharp_peer_send_message(_handle, &arguments);
        }
    }
}