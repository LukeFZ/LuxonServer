using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LuxonServer.Interop;

namespace LuxonServer;

internal static unsafe class ObjectManager
{
    private static bool _registered;

    public static void RegisterObjectManager()
    {
        if (_registered)
            return;

        var objectManagerInterface = new ObjectManagerInterface
        {
            DestroyObject = &DestroyObject,
            FreeAllocation = &FreeAllocation,
        };
        NativeMethods.luxon_csharp_set_object_manager(&objectManagerInterface);

        _registered = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static void DestroyObject(nint handle)
    {
        GCHandle.FromIntPtr(handle).Free();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static void FreeAllocation(void* ptr)
    {
        Marshal.FreeHGlobal((nint)ptr);
    }
}