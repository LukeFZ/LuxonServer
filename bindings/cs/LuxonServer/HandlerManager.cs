using LuxonServer.Interop;
using LuxonServer.Models;
using LuxonServer.Serialization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

public static unsafe class HandlerManager
{
    private static readonly Dictionary<string, Func<HandlerBase>> Factories = [];

    internal static void RegisterServer(string name, Func<HandlerBase> factory)
    {
        NativeInterface.EnsureRegistered();

        Factories[name] = factory;
        // TODO: This should also register the handler with the API, but this currently requires a server context
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static ObjectHandle CreateHandlerInstance(byte* namePtr, PeerHandle peer)
    {
        var name = Marshal.PtrToStringUTF8((nint)namePtr)!;

        var server = Factories[name]();
        server.SetPeer(new Peer(peer));

        return server.ToNativeHandle();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void DestroyHandlerInstance(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static FunctionResult HandleConnect(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleConnect();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static FunctionResult HandleDisconnect(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleDisconnect();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static FunctionResult HandleUpdate(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleUpdate();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static FunctionResult HandleSlowUpdate(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleSlowUpdate();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static FunctionResult HandleOperationRequest(ObjectHandle handle,
        NativeOperationRequestMessage* message, byte isEncrypted, EnetCommandHeader* header)
    {
        var data = new ReadOnlySpan<byte>(message->SerializedParameters, (int)message->SerializedParametersLength);
        var parsed = ValueSerialization.DeserializeAs<Dictionary<byte, object?>>(data);
        var operationRequestMessage = new OperationRequestMessage(message->OperationCode, parsed);

        var isEncryptedBool = isEncrypted == 1;
        var enetHeader = *header;

        return handle.ToManagedObject<HandlerBase>().HandleOperationRequest(operationRequestMessage, isEncryptedBool, enetHeader);
    }
}