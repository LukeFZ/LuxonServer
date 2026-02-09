using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LuxonServer.Interop;

namespace LuxonServer;

public abstract class HandlerBase : IDisposable
{
    private static bool _registered;

    protected virtual void HandleConnect()
    {

    }

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
            HandleConnect = &HandleConnect
        };
        NativeMethods.luxon_csharp_set_server_handler(&serverHandlerInterface);

        _registered = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void HandleConnect(ObjectHandle handle)
    {

    }
}