using LuxonServer.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LuxonServer.Interop;

// Struct size and layout needs to match with the C++ side
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct PluginManagerInterface
{
    private const int FunctionPointerCount = 6;

    internal delegate* unmanaged[Cdecl]<byte*, ObjectHandle> CreatePluginInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, void> DestroyPluginInstance;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnAttach;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, NativeOperationRequestMessage*, NativeOnCreateGameCallInfo*, PluginResult> OnCreateGame;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> BeforeJoin;
    internal delegate* unmanaged[Cdecl]<ObjectHandle, PluginResult> OnJoinGame;

#if DEBUG
    static PluginManagerInterface()
    {
        Debug.Assert(Unsafe.SizeOf<PluginManagerInterface>() == sizeof(nuint) * FunctionPointerCount);
    }
#endif
}