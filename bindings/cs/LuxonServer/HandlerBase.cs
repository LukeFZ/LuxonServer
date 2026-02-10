using LuxonServer.Interop;
using LuxonServer.Models;
using LuxonServer.Serialization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

public abstract class HandlerBase : IDisposable
{
    private static bool _registered;

    protected Peer Peer { get; private set; } = null!;

    protected virtual FunctionResult HandleConnect() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleDisconnect() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleUpdate() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleSlowUpdate() => FunctionResult.CallBase;

    protected virtual FunctionResult HandleOperationRequest(OperationRequestMessage message, bool isEncrypted,
        in EnetCommandHeader header) => FunctionResult.CallBase;

    protected virtual void Dispose(bool disposing)
    {

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    internal void SetPeer(Peer peer)
    {
        Peer = peer;
    }

    internal static unsafe void Register()
    {
        if (_registered)
            return;

        var serverHandlerInterface = new ServerHandlerInterface
        {
            DestroyHandlerInstance = &DestroyHandlerInstance,
            HandleConnect = &HandleConnect,
            HandleDisconnect = &HandleDisconnect,
            HandleUpdate = &HandleUpdate,
            HandleSlowUpdate = &HandleSlowUpdate,
            HandleOperationRequest = &HandleOperationRequest
        };
        NativeMethods.luxon_csharp_set_server_handler(&serverHandlerInterface);

        _registered = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DestroyHandlerInstance(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static FunctionResult HandleConnect(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleConnect();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static FunctionResult HandleDisconnect(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleDisconnect();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static FunctionResult HandleUpdate(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleUpdate();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static FunctionResult HandleSlowUpdate(ObjectHandle handle)
        => handle.ToManagedObject<HandlerBase>().HandleSlowUpdate();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe FunctionResult HandleOperationRequest(ObjectHandle handle,
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