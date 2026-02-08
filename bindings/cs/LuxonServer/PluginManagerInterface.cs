using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer;

// Struct size and layout needs to match with the C++ side
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct PluginManagerInterface
{
    internal delegate* unmanaged[Cdecl]<byte*, ObjectHandle> CreatePluginInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, void> DestroyPluginInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnAttach;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnCreateGame;

#if DEBUG
    static PluginManagerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<PluginManagerInterface>() == sizeof(nuint) * 4);
    }
#endif
}