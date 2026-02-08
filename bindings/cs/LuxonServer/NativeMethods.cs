using System.Runtime.InteropServices;

namespace LuxonServer;

internal static unsafe partial class NativeMethods
{
    private const string LibraryName = "luxon_server";

    // Server

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
        ushort port, [MarshalAs(UnmanagedType.U1)] bool external);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_server_context_destroy(ServerContextHandle handle);

    // Plugin Manager

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_set_plugin_manager(PluginManagerInterface* pluginInterface);

    [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void luxon_csharp_register_plugin(string name);

    // Custom Type Registry

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_register_custom_type(byte code);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_unregister_custom_type(byte code);

    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_set_custom_type_registry(CustomTypeRegistryInterface* registryInterface);

    // Object Manager
    [LibraryImport(LibraryName)]
    internal static partial void luxon_csharp_set_object_manager(ObjectManagerInterface* objectManagerInterface);
}
