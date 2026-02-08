using System.Net;

namespace LuxonServer;

public sealed class ServerContext : IDisposable
{
    internal ServerContextHandle Handle { get; }

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

    public void Configure(string name, IPEndPoint endpoint, bool external = false)
    {
        var address = endpoint.ToString();
        var port = (ushort)endpoint.Port;

        NativeMethods.luxon_csharp_server_context_configure_server(Handle, name, address, port, external);
    }

    public void Dispose()
    {
        NativeMethods.luxon_csharp_server_context_destroy(Handle);
    }
}