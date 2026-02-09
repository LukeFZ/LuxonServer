using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

public sealed class ServerContext : IDisposable
{
    internal ServerContextHandle Handle { get; }

    private static bool _callbackSet;
    private static readonly Dictionary<string, Func<HandlerBase>> _factories = [];

    private ServerContext(ServerContextHandle handle)
    {
        Handle = handle;
    }

    public static ServerContext Create()
    {
        var handle = NativeMethods.luxon_csharp_server_context_create();
        return new ServerContext(handle);
    }

    public void Run()
    {
        NativeMethods.luxon_csharp_server_context_setup(Handle);
        NativeMethods.luxon_csharp_server_context_run(Handle);
    }

    public void Stop()
    {
        NativeMethods.luxon_csharp_server_context_stop(Handle);
    }

    public void ConfigureServer(string name, IPEndPoint endpoint, bool external = false)
    {
        var address = endpoint.ToString();
        var port = (ushort)endpoint.Port;

        NativeMethods.luxon_csharp_server_context_configure_server(Handle, name, address, port, external);
    }

    public void RegisterServer(string name, Func<HandlerBase> factory)
    {
        if (!_callbackSet)
        {
            unsafe
            {
                HandlerBase.Register();
                NativeMethods.luxon_csharp_set_create_handler_callback(&CreateHandler);
            }

            _callbackSet = true;
        }

        _factories[name] = factory;
        NativeMethods.luxon_csharp_server_context_register_server(Handle, name);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe ObjectHandle CreateHandler(byte* namePtr)
    {
        var name = Marshal.PtrToStringUTF8((nint)namePtr)!;
        var server = _factories[name]();
        return server.ToNativeHandle();
    } 

    public void Dispose()
    {
        NativeMethods.luxon_csharp_server_context_destroy(Handle);
    }
}