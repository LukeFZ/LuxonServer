using LuxonServer.Interop;
using LuxonServer.Models;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

public abstract class HandlerBase : IDisposable
{
    private static bool _registered;

    protected virtual FunctionResult HandleConnect() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleDisconnect() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleUpdate() => FunctionResult.CallBase;
    protected virtual FunctionResult HandleSlowUpdate() => FunctionResult.CallBase;

    protected virtual void Dispose(bool disposing)
    {

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    internal static unsafe void Register()
    {
        if (_registered)
            return;

        var serverHandlerInterface = new ServerHandlerInterface
        {
            HandleConnect = &HandleConnect,
            HandleDisconnect = &HandleDisconnect,
            HandleUpdate = &HandleUpdate,
            HandleSlowUpdate = &HandleSlowUpdate,
        };
        NativeMethods.luxon_csharp_set_server_handler(&serverHandlerInterface);

        _registered = true;
    }

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
}