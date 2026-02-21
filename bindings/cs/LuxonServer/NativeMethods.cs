using LuxonServer.Interop;
using System.Runtime.InteropServices;
using LuxonServer.Models;

namespace LuxonServer;

internal static unsafe partial class NativeMethods
{
    private const string LibraryName = "luxon_server";

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_set_interop_interface(InteropInterface* interopInterface);

    [LibraryImport(LibraryName)]
    internal static partial void* luxon_csharp_malloc(nuint size);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_free(void* ptr);

    [LibraryImport(LibraryName)]
    internal static partial ServerContextHandle luxon_csharp_server_context_create();

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_server_context_run(ServerContextHandle handle);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_server_context_setup(ServerContextHandle handle);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_server_context_stop(ServerContextHandle handle);

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void luxon_csharp_server_context_configure_server(ServerContextHandle handle, string name, string address,
        ushort port, ServerProtocol protocol);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_server_context_destroy(ServerContextHandle handle);

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void luxon_csharp_server_context_register_server(ServerContextHandle handle, string name);

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void luxon_csharp_register_plugin(string name);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_peer_send_message(PeerHandle handle, SendMessageArguments* arguments);
}
