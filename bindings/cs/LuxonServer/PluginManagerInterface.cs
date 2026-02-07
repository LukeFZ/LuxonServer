using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

using PluginInstanceHandle = nuint;

// Struct size and layout needs to match with the C++ side
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct PluginManagerInterface
{
    internal delegate* unmanaged[Cdecl]<byte*, PluginInstanceHandle> CreatePluginInstance;
    internal delegate* unmanaged[Cdecl]<PluginInstanceHandle, void> DestroyPluginInstance;
    internal delegate* unmanaged[Cdecl]<PluginInstanceHandle, PluginResult> OnAttach;
    internal delegate* unmanaged[Cdecl]<PluginInstanceHandle, PluginResult> OnCreateGame;

    static PluginManagerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<PluginManagerInterface>() == sizeof(nuint) * 4);
    }
}